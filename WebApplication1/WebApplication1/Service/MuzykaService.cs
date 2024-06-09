using WebApplication1.DTOs;
using WebApplication1.Entities;
using WebApplication1.Repository;

namespace WebApplication1.Service;

public class MuzykaService: IMuzykaService
{
    private readonly IMuzykaRepository _muzykaRepository;

    public MuzykaService(IMuzykaRepository muzykaRepository)
    {
        _muzykaRepository = muzykaRepository;
    }

    public async Task<Muzyk?> CheckMuzyk(int id, CancellationToken token)
    {
        return await _muzykaRepository.CheckMuzyk(id, token);
    }

    public async Task<MuzykDTO> GetMuzykWithUtwors(int id, CancellationToken token)
    {
        Muzyk? muzyk = await CheckMuzyk(id, token);
        if (muzyk == null)
        {
            return new MuzykDTO();
        }

        List<UtworDTO> utworDtos = new List<UtworDTO>();
        foreach (var v in muzyk.IdUtwors)
        {
            utworDtos.Add(new UtworDTO
            {
                NazwaUtworu = v.NazwaUtworu,
                CzasTrwania = v.CzasTrwania
            });
        }

        return new MuzykDTO()
        {
            Imie = muzyk.Imie,
            Nazwisko = muzyk.Nazwisko,
            Pseudonim = muzyk.Pseudonim,
            Utwory = utworDtos
        };
    }

    public async Task<string> AddMuzykAdnUtwor(AddDto addDto, CancellationToken token)
    {
        int message = await _muzykaRepository.AddMuzykAdnUtwor(addDto, token);

        switch (message)
        {
            case -1:
                return "Error: Muzyk już jest stworzony.";
            case -2:
                return "Error: Transkacja zrollbackowana.";
            default:
                return "Wszystko dodane poprawnie.";
        }
    }
}