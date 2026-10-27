using Newtonsoft.Json;

namespace Talleres.Logica.Data;

public class TallerRepository
{
    private readonly string _ruta;

    public TallerRepository(string ruta = "talleres.json")
    {
        _ruta = ruta;
        if (!File.Exists(_ruta))
        {
            var iniciales = new List<Taller>
            {
                new Taller { Id = 1, Nombre = "Robótica", Dia = "Lunes", HoraInicio = 18, HoraFin = 20, Cupo = 3 },
                new Taller { Id = 2, Nombre = "Fotografía", Dia = "Lunes", HoraInicio = 20, HoraFin = 22, Cupo = 2 },
                new Taller { Id = 3, Nombre = "Ajedrez", Dia = "Lunes", HoraInicio = 19, HoraFin = 21, Cupo = 4 },
                new Taller { Id = 4, Nombre = "Teatro", Dia = "Miércoles", HoraInicio = 18, HoraFin = 20, Cupo = 2 }
            };
            File.WriteAllText(_ruta, JsonConvert.SerializeObject(iniciales, Formatting.Indented));
        }
    }

    public List<Taller> ObtenerTodos()
    {
        var json = File.ReadAllText(_ruta);
        return JsonConvert.DeserializeObject<List<Taller>>(json) ?? new List<Taller>();
    }

    public Taller? ObtenerPorId(int id)
    {
        return ObtenerTodos().FirstOrDefault(t => t.Id == id);
    }
}
