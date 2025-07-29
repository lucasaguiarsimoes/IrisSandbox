using IrisSandbox.Models.Interfaces;

namespace IrisSandbox.Models.Common
{
    /// <summary>
    /// Classe que representa uma entidade do sistema na base de dados
    /// </summary>
    public class Entity<TEntity> : Entity, IEntity
        where TEntity : IEntity, new()
    {
    }

    public class Entity : IEntity
    {
        /// <summary>
        /// Identificador único gerado pela base de dados
        /// </summary>
        public long? ID { get; set; }
    }
}
