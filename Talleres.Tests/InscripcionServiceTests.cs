using Talleres.Logica;
using Talleres.Logica.Data;

namespace Talleres.Tests;

public class InscripcionServiceTests
{
    private string _rutaTalleres = string.Empty;
    private string _rutaInscripciones = string.Empty;
    private InscripcionService _service = null!;

    [SetUp]
    public void Setup()
    {
        _rutaTalleres = Path.Combine(Path.GetTempPath(), $"talleres_{Guid.NewGuid()}.json");
        _rutaInscripciones = Path.Combine(Path.GetTempPath(), $"inscripciones_{Guid.NewGuid()}.json");
        _service = new InscripcionService(new InscripcionRepository(_rutaInscripciones), new TallerRepository(_rutaTalleres));
    }

    [TearDown]
    public void TearDown()
    {
        File.Delete(_rutaTalleres);
        File.Delete(_rutaInscripciones);
    }

    [Test]
    public void Inscribir_TallerSinCupo_LanzaReglaNegocioException()
    {
        _service.Inscribir(4, 40111222, "Juan Pérez");
        _service.Inscribir(4, 40333444, "Ana Gómez");

        Assert.Throws<ReglaNegocioException>(() => _service.Inscribir(4, 40555666, "Luis Díaz"));
    }

    [Test]
    public void Inscribir_TallerQueEmpiezaCuandoTerminaOtro_RegistraInscripcion()
    {
        _service.Inscribir(1, 40111222, "Juan Pérez");

        var inscripcion = _service.Inscribir(2, 40111222, "Juan Pérez");

        Assert.That(inscripcion.TallerId, Is.EqualTo(2));
        Assert.That(inscripcion.FechaBaja, Is.Null);
        Assert.That(_service.ObtenerInscripciones(40111222).Count, Is.EqualTo(2));
    }
}
