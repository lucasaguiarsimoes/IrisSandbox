using IrisSandbox.Configuration.Common;
using IrisSandbox.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IrisSandbox.Configuration
{
    public class AmostraExameConfig : EntityDefaultVersionedBaseConfig<AmostraExame>
    {
        public override void ConfigureEntity(EntityTypeBuilder<AmostraExame> builder)
        {
            builder.Property(ae => ae.AmostraID)
                .IsRequired();

            builder.Property(ae => ae.ExameID)
                .IsRequired();

            builder.Property(ae => ae.DataHoraProducao);

            builder.HasAlternateKey(ae => new { ae.AmostraID, ae.ExameID });

            builder.HasOne(ae => ae.Amostra)
               .WithMany(am => am!.Exames)
               .HasForeignKey(ae => ae.AmostraID)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ae => ae.Exame)
                .WithMany(at => at!.Amostras)
                .HasForeignKey(ae => ae.ExameID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(ae => ae.AmostraID);
            builder.HasIndex(ae => ae.ExameID);
            builder.HasIndex(ae => ae.DataHoraProducao);

            builder.ToTable("AmostraExame");
        }
    }
}
