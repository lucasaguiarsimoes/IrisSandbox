using IrisSandbox.Configuration.Interfaces;
using IrisSandbox.Models.Common;

namespace IrisSandbox.Configuration.Common
{
    public abstract class EntityDefaultBaseConfig<TEntity> : EntityBaseConfig<TEntity>, IEntityDefaultConfig
        where TEntity : EntityDefault<TEntity>, new()
    {
    }
}
