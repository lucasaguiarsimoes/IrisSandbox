using IrisSandbox.Models.Interfaces;

namespace IrisSandbox.Models.Common
{
    /// <summary>
    /// Classe que representa uma entidade principal na base de dados versionada para tratamento de concorrência em edições
    /// </summary>
    public class EntityDefaultVersioned<TEntity> : EntityVersioned<TEntity>, IEntityDefaultVersioned
        where TEntity : IEntityDefaultVersioned, new()
    {
    }
}
