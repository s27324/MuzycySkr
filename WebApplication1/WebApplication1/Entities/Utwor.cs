namespace WebApplication1.Entities;

public class Utwor
{
    public int IdUtwor { get; set; }
    public string NazwaUtworu { get; set; }
    public float CzasTrwania { get; set; }
    
    public virtual ICollection<Muzyk> IdMuzyks { get; set; } = new List<Muzyk>();
}