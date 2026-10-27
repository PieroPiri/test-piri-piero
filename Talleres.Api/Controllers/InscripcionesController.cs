using Microsoft.AspNetCore.Mvc;
using Talleres.Api.DTOs;
using Talleres.Logica;

namespace Talleres.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InscripcionesController : ControllerBase
{
    private readonly InscripcionService _service;

    public InscripcionesController(InscripcionService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult ObtenerTodas([FromQuery] int? dni)
    {
        var respuesta = _service.ObtenerInscripciones(dni).Select(ARespuesta).ToList();
        return Ok(respuesta);
    }

    [HttpPost]
    public IActionResult Crear([FromBody] InscripcionRequest request)
    {
        try
        {
            var inscripcion = _service.Inscribir(request.TallerId!.Value, request.Dni!.Value, request.NombreAlumno.Trim());
            return CreatedAtAction(nameof(ObtenerTodas), ARespuesta(inscripcion));
        }
        catch (NoEncontradoException ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
        catch (ReglaNegocioException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpPut("{id}/baja")]
    public IActionResult DarDeBaja(int id)
    {
        try
        {
            var inscripcion = _service.DarDeBaja(id);
            return Ok(ARespuesta(inscripcion));
        }
        catch (NoEncontradoException ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
        catch (ReglaNegocioException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    private static InscripcionResponse ARespuesta(Inscripcion inscripcion)
    {
        return new InscripcionResponse
        {
            Id = inscripcion.Id,
            TallerId = inscripcion.TallerId,
            Dni = inscripcion.Dni,
            NombreAlumno = inscripcion.NombreAlumno,
            FechaInscripcion = inscripcion.FechaInscripcion,
            FechaBaja = inscripcion.FechaBaja,
            Activa = inscripcion.EstaActiva
        };
    }
}
