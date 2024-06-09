using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Entities.Configs;

public class MuzykEfConfiguration: IEntityTypeConfiguration<Muzyk>
{
    public void Configure(EntityTypeBuilder<Muzyk> builder)
    {
        builder
            .HasKey(x => x.IdMuzyk)
            .HasName("Muzyk_pk");

        builder
            .Property(x => x.IdMuzyk)
            .UseIdentityColumn();
        builder
            .Property(x => x.Imie)
            .IsRequired()
            .HasMaxLength(30);
        builder
            .Property(x => x.Nazwisko)
            .IsRequired()
            .HasMaxLength(40);
        builder
            .Property(x => x.Pseudonim)
            .HasMaxLength(50);

        builder.ToTable(nameof(Muzyk));

        Muzyk[] muzyks =
        {
            new Muzyk()
            {
                IdMuzyk = 1, Imie = "Krzysztof", Nazwisko = "Krawczyk", Pseudonim = null
            },
            new Muzyk()
            {
                IdMuzyk = 2, Imie = "Kamil", Nazwisko = "Pilarzyk", Pseudonim = "Kasztan"
            }
        };

        builder.HasData(muzyks);

        builder
            .HasMany(x => x.IdUtwors)
            .WithMany(x => x.IdMuzyks)
            .UsingEntity<Dictionary<string, object>>(
                "MuzykUtwor",
                x => x.HasOne<Utwor>().WithMany().HasForeignKey("IdUtwor").OnDelete(DeleteBehavior.Restrict),
                x => x.HasOne<Muzyk>().WithMany().HasForeignKey("IdMuzyk").OnDelete(DeleteBehavior.Restrict))
            .HasData(
                new { IdMuzyk = 1, IdUtwor = 1 },
                new { IdMuzyk = 2, IdUtwor = 2 }
                );
    }
}