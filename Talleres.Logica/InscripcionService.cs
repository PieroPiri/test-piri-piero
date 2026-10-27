using Talleres.Logica.Data;

namespace Talleres.Logica;

public class InscripcionService
{
    private const int MaximoInscripcionesActivas = 2;

    private readonly InscripcionRepository _inscripcionRepository;
    private readonly TallerRepository _tallerRepository;

    public InscripcionService(InscripcionRepository inscripcionRepository, TallerRepository tallerRepository)
    {
        _inscripcionRepository = inscripcionRepository;
        _tallerRepository = tallerRepository;
    }

    public List<TallerConCupo> ObtenerTalleres()
    {
        var activas = _inscripcionRepository.ObtenerTodas().Where(i => i.EstaActiva).ToList();
        return _tallerRepository.ObtenerTodos()
            .Select(t => new TallerConCupo
            {
                Taller = t,
                CuposDisponibles = t.Cupo - activas.Count(i => i.TallerId == t.Id)
            })
            .ToList();
    }

    public List<Inscripcion> ObtenerInscripciones(int? dni)
    {
        var inscripciones = _inscripcionRepository.ObtenerTodas();
        if (dni.HasValue)
        {
            return inscripciones.Where(i => i.Dni == dni.Value).ToList();
        }
        return inscripciones;
    }

    public Inscripcion Inscribir(int tallerId, int dni, string nombreAlumno)
    {
        var taller = _tallerRepository.ObtenerPorId(tallerId);
        if (taller == null)
        {
            throw new NoEncontradoException($"El taller {tallerId} no existe.");
        }

        var activas = _inscripcionRepository.ObtenerTodas().Where(i => i.EstaActiva).ToList();

        if (activas.Count(i => i.TallerId == tallerId) >= taller.Cupo)
        {
            throw new ReglaNegocioException($"El taller {taller.Nombre} no tiene cupo disponible.");
        }

        var activasDelAlumno = activas.Where(i => i.Dni == dni).ToList();

        if (activasDelAlumno.Any(i => i.TallerId == tallerId))
        {
            throw new ReglaNegocioException($"El alumno con DNI {dni} ya está inscripto al taller {taller.Nombre}.");
        }

        if (activasDelAlumno.Count >= MaximoInscripcionesActivas)
        {
            throw new ReglaNegocioException($"El alumno con DNI {dni} ya tiene {MaximoInscripcionesActivas} inscripciones activas.");
        }

        var talleres = _tallerRepository.ObtenerTodos();
        var superpuesto = activasDelAlumno
            .Select(i => talleres.FirstOrDefault(t => t.Id == i.TallerId))
            .FirstOrDefault(t => t != null && t.SeSuperponeCon(taller));
        if (superpuesto != null)
        {
            throw new ReglaNegocioException($"El taller {taller.Nombre} se superpone con {superpuesto.Nombre}, en el que el alumno ya está inscripto.");
        }

        var inscripcion = new Inscripcion
        {
            TallerId = tallerId,
            Dni = dni,
            NombreAlumno = nombreAlumno,
            FechaInscripcion = DateTime.Now,
            FechaBaja = null
        };
        return _inscripcionRepository.Agregar(inscripcion);
    }

    public Inscripcion DarDeBaja(int inscripcionId)
    {
        var inscripcion = _inscripcionRepository.ObtenerPorId(inscripcionId);
        if (inscripcion == null)
        {
            throw new NoEncontradoException($"La inscripción {inscripcionId} no existe.");
        }

        if (!inscripcion.EstaActiva)
        {
            throw new ReglaNegocioException($"La inscripción {inscripcionId} ya fue dada de baja.");
        }

        inscripcion.FechaBaja = DateTime.Now;
        _inscripcionRepository.Actualizar(inscripcion);
        return inscripcion;
    }
}
