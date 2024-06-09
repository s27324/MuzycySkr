using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Service;

namespace WebApplication1.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MuzykaController: ControllerBase
{
    private readonly IMuzykaService _muzykaService;

    public MuzykaController(IMuzykaService muzykaService)
    {
        _muzykaService = muzykaService;
    }

    [HttpGet("{idMuzyk}")]
    public async Task<IActionResult> GetMuzyk(int idMuzyk, CancellationToken token)
    {
        return Ok(await _muzykaService.GetMuzykWithUtwors(idMuzyk, token));
    }

    [HttpPost]
    public async Task<IActionResult> PostMuzyk(AddDto addDto, CancellationToken token)
    {
        string message = await _muzykaService.AddMuzykAdnUtwor(addDto, token);

        if (message.StartsWith("Error"))
        {
            return NotFound(message);
        }
        return Ok(message);
    }
}