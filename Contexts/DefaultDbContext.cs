using IrisSandbox.Configuration.Interfaces;
using IrisSandbox.Connection.Interfaces;
using IrisSandbox.Contexts.Common;
using IrisSandbox.Contexts.Interfaces;
using IrisSandbox.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Matrix.LAS.Database.Context
{
    /// <summary>
    /// Contexto de conexões do sistema com logger para produção
    /// </summary>
    public class DefaultDbContext : DbContextBase, IEntityContextDefault
    {
        public override Type ConfigType => typeof(IEntityDefaultConfig);

        public override Type ContextType  => typeof(IEntityDefault);

        /// <summary>
        /// O construtor vazio é necessário para realizar o Command add-migration em tempo de compilação
        /// </summary>
        public DefaultDbContext()
        {
        }

        public DefaultDbContext(IDbSandboxConnectionDefault Conexao, ILoggerFactory? logger = null)
            : base(logger)
        {
            this.Conexao = Conexao;
        }

        public async Task ApplyChangesAsync(CancellationToken cancellationToken)
        {
            // Aciona o salvamento dos dados sem o accept changes para manter os registros realizados
            await this.SaveChangesAsync(cancellationToken);
        }

        public override void OnModelCreatingSpecialized(ModelBuilder modelBuilder)
        {
        }
    }
}
