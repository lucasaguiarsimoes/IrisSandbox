using IrisSandbox.Configuration.Interfaces;
using IrisSandbox.Models.Common;

namespace IrisSandbox.Configuration.Common
{
    public abstract class EntityDefaultVersionedBaseConfig<TEntity> : EntityVersionedBaseConfig<TEntity>, IEntityDefaultConfig
        where TEntity : EntityDefaultVersioned<TEntity>, new()
    {
    }
}
