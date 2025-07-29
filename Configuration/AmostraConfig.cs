using IrisSandbox.Configuration.Common;
using IrisSandbox.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IrisSandbox.Configuration
{
    public class AmostraConfig : EntityDefaultVersionedBaseConfig<Amostra>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Amostra> builder)
        {
            builder.Property(a => a.Identificacao)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.Pedido)
                .HasMaxLength(50);

            builder.Property(a => a.Origem)
                .HasMaxLength(50);

            builder.Property(a => a.Volume)
                .HasColumnType("decimal(12, 7)");

            builder.HasIndex(a => a.Identificacao)
                .IsUnique();

            builder.ToTable("Amostra");
        }
    }
}
