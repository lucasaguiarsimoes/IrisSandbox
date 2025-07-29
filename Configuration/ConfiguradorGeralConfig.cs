using IrisSandbox.Configuration.Common;
using IrisSandbox.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IrisSandbox.Configuration
{
    public class ConfiguradorGeralConfig : EntityDefaultVersionedBaseConfig<ConfiguradorGeral>
    {
        public override void ConfigureEntity(EntityTypeBuilder<ConfiguradorGeral> builder)
        {
            builder.Property(c => c.Item)
                .IsRequired();

            builder.Property(c => c.ValorString)
                .HasMaxLength(2066);

            builder.Property(c => c.ValorBoolean);

            builder.Property(c => c.ValorBytes);

            builder.Property(c => c.ValorDateTime);

            builder.Property(c => c.ValorDecimal)
                .HasColumnType("decimal(18, 6)");

            builder.Property(c => c.ValorLong);

            builder.HasIndex(c => c.Item)
                .IsUnique();

            builder.ToTable("ConfiguradorGeral");
        }
    }
}
