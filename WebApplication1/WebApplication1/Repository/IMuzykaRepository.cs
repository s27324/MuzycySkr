using WebApplication1.DTOs;
using WebApplication1.Entities;

namespace WebApplication1.Repository;

public interface IMuzykaRepository
{
    public Task<Muzyk?> CheckMuzyk(int id, CancellationToken token);

    public Task<bool> IsMuzykInDb(string imie, string nazwisko, string? pseudonim, CancellationToken token);
    public Task<bool> IsUtworInDb(string nazwaUtworu, float czasTrwania, CancellationToken token);
    public Task<int> IdUtworInDb(string nazwaUtworu, float czasTrwania, CancellationToken token);
    public Task<int> IdMuzykInDb(string imie, string nazwisko, string? pseudonim, CancellationToken token);
    public Task<int> AddMuzykAdnUtwor(AddDto addDto, CancellationToken token);
}