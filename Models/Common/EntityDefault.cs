using IrisSandbox.Models.Interfaces;

namespace IrisSandbox.Models.Common
{
    /// <summary>
    /// Classe que representa uma entidade principal na base de dados
    /// </summary>
    public class EntityDefault<TEntity> : Entity<TEntity>, IEntityDefault
        where TEntity : IEntityDefault, new()
    {
    }
}
