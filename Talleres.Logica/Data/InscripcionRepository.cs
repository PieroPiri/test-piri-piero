using Newtonsoft.Json;

namespace Talleres.Logica.Data;

public class InscripcionRepository
{
    private readonly string _ruta;

    public InscripcionRepository(string ruta = "inscripciones.json")
    {
        _ruta = ruta;
    }

    public List<Inscripcion> ObtenerTodas()
    {
        if (!File.Exists(_ruta))
        {
            return new List<Inscripcion>();
        }
        var json = File.ReadAllText(_ruta);
        return JsonConvert.DeserializeObject<List<Inscripcion>>(json) ?? new List<Inscripcion>();
    }

    public Inscripcion? ObtenerPorId(int id)
    {
        return ObtenerTodas().FirstOrDefault(i => i.Id == id);
    }

    public Inscripcion Agregar(Inscripcion inscripcion)
    {
        var inscripciones = ObtenerTodas();
        inscripcion.Id = inscripciones.Count > 0 ? inscripciones.Max(i => i.Id) + 1 : 1;
        inscripciones.Add(inscripcion);
        Guardar(inscripciones);
        return inscripcion;
    }

    public void Actualizar(Inscripcion inscripcion)
    {
        var inscripciones = ObtenerTodas();
        var indice = inscripciones.FindIndex(i => i.Id == inscripcion.Id);
        if (indice >= 0)
        {
            inscripciones[indice] = inscripcion;
            Guardar(inscripciones);
        }
    }

    private void Guardar(List<Inscripcion> inscripciones)
    {
        File.WriteAllText(_ruta, JsonConvert.SerializeObject(inscripciones, Formatting.Indented));
    }
}
