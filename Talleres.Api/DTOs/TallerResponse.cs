namespace Talleres.Api.DTOs;

public class TallerResponse
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Dia { get; set; } = string.Empty;
    public int HoraInicio { get; set; }
    public int HoraFin { get; set; }
    public int Cupo { get; set; }
    public int CuposDisponibles { get; set; }
}
