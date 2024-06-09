using WebApplication1.DTOs;
using WebApplication1.Entities;

namespace WebApplication1.Service;

public interface IMuzykaService
{
    public Task<Muzyk?> CheckMuzyk(int id, CancellationToken token);

    public Task<MuzykDTO> GetMuzykWithUtwors(int id, CancellationToken token);
    
    public Task<string> AddMuzykAdnUtwor(AddDto addDto, CancellationToken token);
}