using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Collections;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Microsoft.Extensions.Logging;
using IrisSandbox.Connection.Interfaces;
using IrisSandbox.Contexts.Interfaces;
using IrisSandbox.Models.Interfaces;
using IrisSandbox.Configuration.Interfaces;
using IrisSandbox.Extensions;

namespace IrisSandbox.Contexts.Common
{
    /// <summary>
    /// DbContext padrão do sistema na comunicação com o banco de dados
    /// </summary>
    public abstract class DbContextBase : DbContext, IEntityContext
    {
        /// <summary>
        /// Factory do logger do sistema
        /// </summary>
        protected readonly ILoggerFactory? _logger;

        /// <summary>
        /// Conexão com o banco de dados
        /// </summary>
        protected IDbSandboxConnection? Conexao { get; set; }

        /// <summary>
        /// Controle para verificar conexão aberta
        /// </summary>
        private bool _IsConnectionOpen { get; set; } = false;

        /// <summary>
        /// Configuração utilizada em tempo de Design para criação de migrations
        /// </summary>
        public static IConfiguration? ConfigurationMigrationArguments { get; set; }

        /// <summary>
        /// Nome do Collation de Case Insensitive no POSTGRESQL
        /// </summary>
        public const string COLLATION_CASEINSENSITIVE_POSTGRESQL = "case_insensitive";

        /// <summary>
        /// Timeout para execução de migrations
        /// </summary>
        public static readonly TimeSpan COMMAND_TIMEOUT_MIGRATIONS = TimeSpan.FromHours(24);

        /// <summary>
        /// Todos os possíveis Types de config que podem ser utilizado por Contextos
        /// ATENÇÃO: Caso um novo contexto seja implementado, sua interface de Configuration deve ser incluída aqui.
        /// </summary>
        protected readonly Type[] EntitiesTypes = new[]
        {
            typeof(IEntityDefaultConfig),
            typeof(IEntityHistoryConfig)
        };

        /// <summary>
        /// Define qual o tipo de Config esperado para o Contexto
        /// </summary>
        public abstract Type ConfigType { get; }

        /// <summary>
        /// Define qual o tipo de Contexto esperado
        /// </summary>
        public abstract Type ContextType { get; }

        /// <summary>
        /// Construtor vazio dedicado à construção de migrations
        /// </summary>
        public DbContextBase()
        {
        }

        public DbContextBase(ILoggerFactory? logger)
        {
            this._logger = logger;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Se não tiver conexão, o acionamento foi feito para a criação de migrations em tempo de desenvolvimento
            if (this.Conexao == null)
            {
                optionsBuilder.UseIris("Server=localhost; Port=51773; Namespace=DUMMY; Password=SYS; User ID=_SYSTEM;");
                return;
            }

            // Se chegou aqui, executa a lógica normal de um contexto com conexão
            this.UseDatabaseEF(optionsBuilder);

            // AO DESENVOLVEDOR: Para possibilitar um log detalhado de Erros
            //optionsBuilder.EnableDetailedErrors();
            //optionsBuilder.EnableSensitiveDataLogging();
        }

        private void UseDatabaseEF(DbContextOptionsBuilder optionsBuilder)
        {
            // Aqui sempre deverá ter conexão, mas mesmo assim faz uma proteção por garantia
            if (this.Conexao != null)
            {
                // Aplica o uso do banco de dados especificado
                this.Conexao.UseDatabaseEF(optionsBuilder);
            }

            // Aqui sempre deverá ter logger, mas mesmo assim faz uma proteção por garantia
            if (this._logger != null)
            {
                // Aplica o uso do logger especificado
                optionsBuilder.UseLoggerFactory(this._logger);
            }
        }

        public DbConnection UseConnection()
        {
            // Não há como utilizar uma conexão que não foi previamente definida
            if (this.Conexao == null)
            {
                throw new Exception("Empty Connection");
            }

            // Tenta abrir a conexão apenas se ela ainda não estiver aberta. Isto é, se ainda estiver no primeiro uso
            if (!this._IsConnectionOpen)
            {
                try
                {
                    // Verifica se algum erro ocorre na tentativa de abrir a conexão com o banco de dados
                    this._IsConnectionOpen = this.Conexao.Abre();
                }
                catch (Exception exc)
                {
                    throw new Exception("Connection Failed", exc);
                }

                // Valida se a conexão foi realmente aberta
                if (!this._IsConnectionOpen)
                {
                    throw new Exception("Connection Failed");
                }
            }

            // Retorna a conexão em aberto
            return this.Conexao.Connection;
        }

        public async virtual Task MigrateAsync(CancellationToken cancellationToken)
        {
            // Verifica se é possivel se conectar com a database para garantir que a mesma existe e está acessivel
            if (!await this.Database.CanConnectAsync(cancellationToken))
            {
                throw new Exception("Connection Failed");
            }

            await ExecuteMigrationWithTimeout(async () => await this.Database.MigrateAsync(cancellationToken));
        }

        private async Task ExecuteMigrationWithTimeout(Func<Task> executionMethod)
        {
            // Pega o timeout de comandos atual válido para todo o sistema
            int? currentTimeout = this.Database.GetCommandTimeout();

            // Altera o timeout para um maior para garantir a execução dos migrations
            this.Database.SetCommandTimeout(COMMAND_TIMEOUT_MIGRATIONS);

            try
            {
                // Executa a ação de migration
                await executionMethod();
            }
            finally
            {
                // Volta para o timeout padrão existente após a execução do migration
                this.Database.SetCommandTimeout(currentTimeout);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cria e aplica dinamicamente todos os configurations dos models previstos no modelTypes
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(), t => ContextModelTypesFilter(t));

            // Cria e aplica dinamicamente todos os configuration dos models de acordo com o banco de dados utilizado
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(), t => ContextModelTypesFilter(t));

            // Aplica conversão para todos os tipos de data do sistema (EF Core)
            modelBuilder.UseValueConverters(this);

            // Se necessário, lida com as diferenças dos DbContexts
            this.OnModelCreatingSpecialized(modelBuilder);
        }

        /// <summary>
        /// Se necessário, trata de forma específica o DbContext
        /// </summary>
        public virtual void OnModelCreatingSpecialized(ModelBuilder modelBuilder)
        {
            // Por padrão, não faz nada
            return;
        }

        /// <summary>
        /// Define como deve ser encontrado um ModelConfig (Configuração de modelo do EF Core)
        /// </summary>
        public virtual bool ContextModelTypesFilter(Type modelConfigType)
        {
            // Guarda o Type de config esperado por esse Contexto
            Type contextModelConfigType = this.ConfigType;

            // Guarda o Type de Contexto a qual as entidades devem pertencer
            Type contextModelType = this.ContextType;

            // Verifica se o Type de Config implementa o type informado pela implementação do Contexto
            bool isAssinableAsExpected = contextModelConfigType.IsAssignableFrom(modelConfigType);

            // Verifica se o Type de Config NÃO implementa os possíveis outros Types de Configuration
            bool isNotAssignableByAnotherContextConfigration = !this.EntitiesTypes.Except(new[] { contextModelConfigType }).Any(et => et.IsAssignableFrom(modelConfigType));

            // Verifica se o Type é público, é classe e não é abstrato
            bool isClassPublicAndNotAbstract = modelConfigType.IsClass && !modelConfigType.IsAbstract && modelConfigType.IsPublic;

            // Caso qualquer um dos requisitos não seja preenchido quer dizer que este Type não deve ser levado em conta pelo ContextModel
            if (!isAssinableAsExpected || !isNotAssignableByAnotherContextConfigration || !isClassPublicAndNotAbstract)
            {
                return false;
            }

            // Se chegou aqui quer dizer que o Type é uma entidade do Contexto

            // Verifica se existe alguma propriedade ou coleção da entidade que pertence não pertence a este contexto
            if (HasNavigationFromAnotherContext(modelConfigType, contextModelType))
            {
                throw new InvalidOperationException("Modelagem inválida detectada durante a validação das entidades do sistema. Verifique se alguma Navigation não pertence ao mesmo contexto da entidade a qual ela está contida.");
            }

            return true;
        }

        public void DetachContext()
        {
            // Fecha a conexão em uso, caso tenha sido aberta anteriormente
            if (this._IsConnectionOpen && this.Conexao != null)
            {
                this.Conexao?.Fecha();
            }
        }

        public void DetachEntries()
        {
            this.ChangeTracker.Clear();
        }

        public bool HasNavigationFromAnotherContext(Type entityConfigurationType, Type contextType)
        {
            // Busca o Configuration da Entidade e através do argumento genérico dele guarda o Type da entidade em questão
            Type? entityType = entityConfigurationType
                .GetInterfaces()
                .FirstOrDefault(i => i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))?
                .GetGenericArguments()
                .FirstOrDefault();

            if (entityType == null)
            {
                return false;
            }

            // Itera sobre as propriedades do Type
            return entityType.GetProperties().Any(prop =>
            {
                // Guarda o type da propriedae
                var navigationType = prop.PropertyType;

                // Verifica se o Type da propriedade não implementa o contexto esperado
                if (IsEntityAssinableFromAnotherContext(navigationType, contextType))
                {
                    return true;
                }

                // Se chegou aqui quer dizer que pode ser uma coleção (`List<Entity>`)
                // Verifica se é uma List ou IEnumerable
                // Como string implementa IEnumerable<char>, não precisamos deixa-las entrarem on IF
                if (navigationType != typeof(string) &&
                    typeof(IEnumerable).IsAssignableFrom(navigationType))
                {
                    // Guarda o Argumento genérico da coleção
                    var navigationTypeFromCollection = navigationType.GetGenericArguments().FirstOrDefault();

                    // Verifica se é uma entidade do sistema e se não implementa o contexto esperado
                    if (IsEntityAssinableFromAnotherContext(navigationTypeFromCollection, contextType))
                    {
                        return true;
                    }
                }

                return false;
            });
        }

        private static bool IsEntityAssinableFromAnotherContext(Type? type, Type contextType)
        {
            // Confirma se é uma Entidade do sistema
            if (typeof(IEntity).IsAssignableFrom(type))
            {
                // Verifica se ela NÃO implementa a interface esperada
                if (!contextType.IsAssignableFrom(type))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
