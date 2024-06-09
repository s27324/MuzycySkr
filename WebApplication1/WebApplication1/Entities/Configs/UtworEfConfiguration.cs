using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Entities.Configs;

public class UtworEfConfiguration: IEntityTypeConfiguration<Utwor>
{
    public void Configure(EntityTypeBuilder<Utwor> builder)
    {
        builder
            .HasKey(x => x.IdUtwor)
            .HasName("Utwor_pk");

        builder
            .Property(x => x.IdUtwor)
            .UseIdentityColumn();
        builder
            .Property(x => x.NazwaUtworu)
            .IsRequired()
            .HasMaxLength(30);
        builder
            .Property(x => x.CzasTrwania)
            .IsRequired();

        builder.ToTable(nameof(Utwor));

        Utwor[] utwors =
        {
            new Utwor()
            {
                IdUtwor = 1, NazwaUtworu = "Parostatek", CzasTrwania = (float)2.53
            },
            new Utwor()
            {
                IdUtwor = 2, NazwaUtworu = "Koloid: Nowe Pokolenie", CzasTrwania = (float)6.09
            }
        };

        builder.HasData(utwors);
    }
}