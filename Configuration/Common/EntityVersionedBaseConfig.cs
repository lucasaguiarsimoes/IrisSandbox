using IrisSandbox.Models.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IrisSandbox.Configuration.Common
{
    public abstract class EntityVersionedBaseConfig<TEntity> : EntityBaseConfig<TEntity>
        where TEntity : EntityVersioned<TEntity>, new()
    {
        public override void ConfigureBase(EntityTypeBuilder<TEntity> builder)
        {
            base.ConfigureBase(builder);
            builder.Property(c => c.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();
        }
    }
}
