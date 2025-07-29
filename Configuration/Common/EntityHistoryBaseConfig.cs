using IrisSandbox.Configuration.Interfaces;
using IrisSandbox.Models.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IrisSandbox.Configuration.Common
{
    public abstract class EntityHistoryBaseConfig<TEntity> : EntityBaseConfig<TEntity>, IEntityHistoryConfig
        where TEntity : EntityHistory<TEntity>, new()
    {
        public override void ConfigureBase(EntityTypeBuilder<TEntity> builder)
        {
            base.ConfigureBase(builder);

            builder.Property(c => c.ActionGuid)
                .IsRequired();

            builder.Property(c => c.PrimaryID)
                .IsRequired();

            builder.Property(c => c.PrincipalEntityID)
                .IsRequired();

            builder.Property(c => c.EntityName)
                .IsRequired();

            builder.Property(c => c.DataHora)
                .IsRequired();

            builder.Property(c => c.UsuarioID);

            builder.Property(c => c.State)
                .IsRequired();

            builder.Property(c => c.Command)
                .HasMaxLength(100);

            builder.Property(c => c.ForeignKeys);

            builder.Property(c => c.OldValues);

            builder.Property(c => c.NewValues);

            builder.HasIndex(c => c.ActionGuid);
            builder.HasIndex(c => c.PrincipalEntityID);
            builder.HasIndex(c => new { c.EntityName, c.PrimaryID });
        }
    }
}
