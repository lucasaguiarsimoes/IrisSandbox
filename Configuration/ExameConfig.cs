using IrisSandbox.Configuration.Common;
using IrisSandbox.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IrisSandbox.Configuration
{
    public abstract class ExameConfig : EntityDefaultVersionedBaseConfig<Exame>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Exame> builder)
        {
            builder.Property(e => e.Codigo)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.QuantidadeTeste)
                .HasDefaultValue(5)
                .IsRequired();

            builder.Property(e => e.ExibeTeste)
                .HasDefaultValue(true)
                .IsRequired();

            builder.HasIndex(a => a.Codigo)
                .IsUnique();

            builder.ToTable("Exame");
        }
    }
}
