using Newtonsoft.Json;

namespace Talleres.Logica;

public class Inscripcion
{
    public int Id { get; set; }
    public int TallerId { get; set; }
    public int Dni { get; set; }
    public string NombreAlumno { get; set; } = string.Empty;
    public DateTime FechaInscripcion { get; set; }
    public DateTime? FechaBaja { get; set; }

    [JsonIgnore]
    public bool EstaActiva => FechaBaja == null;
}
