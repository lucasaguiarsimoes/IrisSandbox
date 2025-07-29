using IrisSandbox.Configuration.Interfaces;
using IrisSandbox.Connection.Interfaces;
using IrisSandbox.Contexts.Common;
using IrisSandbox.Contexts.Interfaces;
using IrisSandbox.Models.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Matrix.LAS.Database.Context
{
    /// <summary>
    /// Contexto de conexões do sistema para entidades de histórico com logger para produção
    /// </summary>
    public class HistoryDbContext : DbContextBase, IEntityContextHistory
    {
        public override Type ConfigType => typeof(IEntityHistoryConfig);

        public override Type ContextType => typeof(IEntityHistory);

        /// <summary>
        /// O construtor vazio é necessário para realizar o Command add-migration em tempo de compilação
        /// </summary>
        public HistoryDbContext()
        {
        }

        public HistoryDbContext(IDbSandboxConnectionHistory Conexao, ILoggerFactory? logger = null)
            : base(logger)
        {
            this.Conexao = Conexao;
        }

        public virtual async Task ApplyChangesAsync(CancellationToken cancellationToken)
        {
            await this.SaveChangesAsync(cancellationToken);
        }
    }
}
