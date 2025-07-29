using IrisSandbox.Models.Interfaces;

namespace IrisSandbox.Models.Common
{
    /// <summary>
    /// Classe que representa uma entidade do sistema na base de dados versionada para tratamento de concorrência em edições
    /// </summary>
    public class EntityVersioned<TEntity> : Entity<TEntity>, IEntityVersioned
        where TEntity : IEntityVersioned, new()
    {
        /// <summary>
        /// Versão de edição para controle de concorrência
        /// </summary>
        public byte[]? RowVersion { get; set; }
    }

    public class EntityVersioned : Entity, IEntityVersioned
    {
        /// <summary>
        /// Versão de edição para controle de concorrência
        /// </summary>
        public byte[]? RowVersion { get; set; }
    }
}
