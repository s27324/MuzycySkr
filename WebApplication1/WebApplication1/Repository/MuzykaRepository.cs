using Microsoft.EntityFrameworkCore;
using WebApplication1.DTOs;
using WebApplication1.Entities;

namespace WebApplication1.Repository;

public class MuzykaRepository: IMuzykaRepository
{
    private readonly MuzykaDbContext _muzykaDbContext;

    public MuzykaRepository(MuzykaDbContext muzykaDbContext)
    {
        _muzykaDbContext = muzykaDbContext;
    }


    public async Task<Muzyk?> CheckMuzyk(int id, CancellationToken token)
    {
        return await _muzykaDbContext.Muzyks.Include(x => x.IdUtwors).Where(x => x.IdMuzyk == id)
            .FirstOrDefaultAsync(token);
    }

    public async Task<bool> IsMuzykInDb(string imie, string nazwisko, string? pseudonim, CancellationToken token)
    {
        return await _muzykaDbContext.Muzyks.AnyAsync(
            x => x.Pseudonim == pseudonim && x.Imie == imie && x.Nazwisko == nazwisko, token);
    }

    public async Task<bool> IsUtworInDb(string nazwaUtworu, float czasTrwania, CancellationToken token)
    {
        return await _muzykaDbContext.Utwors.AnyAsync(x => x.NazwaUtworu == nazwaUtworu && x.CzasTrwania == czasTrwania,
            token);
    }

    public async Task<int> IdUtworInDb(string nazwaUtworu, float czasTrwania, CancellationToken token)
    {
        return await _muzykaDbContext.Utwors.Where(x => x.NazwaUtworu == nazwaUtworu && x.CzasTrwania == czasTrwania)
            .Select(x => x.IdUtwor).FirstOrDefaultAsync(token);
    }
    public async Task<int> IdMuzykInDb(string imie, string nazwisko, string? pseudonim, CancellationToken token)
    {
        return await _muzykaDbContext.Muzyks.Where(x => x.Imie == imie && x.Nazwisko == nazwisko && x.Pseudonim == pseudonim)
            .Select(x => x.IdMuzyk).FirstOrDefaultAsync(token);
    }

    public async Task<int> AddMuzykAdnUtwor(AddDto addDto, CancellationToken token)
    {
        using var trans = await _muzykaDbContext.Database.BeginTransactionAsync(token);
        try
        {
            bool isMuzykInDb = await IsMuzykInDb(addDto.Imie, addDto.Nazwisko, addDto.Pseudonim, token);
            if (isMuzykInDb)
            {
                return -1;
            }

            await _muzykaDbContext.AddAsync(new Muzyk()
            {
                Imie = addDto.Imie,
                Nazwisko = addDto.Nazwisko,
                Pseudonim = addDto.Pseudonim
            }, token);
            await _muzykaDbContext.SaveChangesAsync(token);

            bool isUtworInDb = await IsUtworInDb(addDto.NazwaUtworu, addDto.CzasTrwania, token);
            if (isUtworInDb == false)
            {
                await _muzykaDbContext.Utwors.AddAsync(new Utwor()
                {
                    NazwaUtworu = addDto.NazwaUtworu,
                    CzasTrwania = addDto.CzasTrwania
                }, token);
                await _muzykaDbContext.SaveChangesAsync(token);
            }
            int idUtwor = await IdUtworInDb(addDto.NazwaUtworu, addDto.CzasTrwania, token);
            int idMuzyk = await IdMuzykInDb(addDto.Imie, addDto.Nazwisko, addDto.Pseudonim, token);

            Muzyk muzyk = await _muzykaDbContext.Muzyks.SingleAsync(x => x.IdMuzyk == idMuzyk, token);
            Utwor utwor = await _muzykaDbContext.Utwors.SingleAsync(x => x.IdUtwor == idUtwor, token);
        
            muzyk.IdUtwors.Add(utwor);
            await _muzykaDbContext.SaveChangesAsync(token);

            await trans.CommitAsync(token);
            return 0;
        }
        catch (Exception e)
        {
            await trans.RollbackAsync(token);
            return -2;
        }
    }
}