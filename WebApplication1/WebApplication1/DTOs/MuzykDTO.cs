namespace WebApplication1.DTOs;

public class MuzykDTO
{
    public string Imie { get; set; }
    public string Nazwisko { get; set; }
    public string? Pseudonim { get; set; }
    public List<UtworDTO> Utwory { get; set; } = new List<UtworDTO>();
}