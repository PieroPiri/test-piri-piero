using Microsoft.AspNetCore.Mvc;
using Talleres.Api.DTOs;
using Talleres.Logica;

namespace Talleres.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TalleresController : ControllerBase
{
    private readonly InscripcionService _service;

    public TalleresController(InscripcionService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult ObtenerTodos()
    {
        var respuesta = _service.ObtenerTalleres().Select(ARespuesta).ToList();
        return Ok(respuesta);
    }

    private static TallerResponse ARespuesta(TallerConCupo tallerConCupo)
    {
        return new TallerResponse
        {
            Id = tallerConCupo.Taller.Id,
            Nombre = tallerConCupo.Taller.Nombre,
            Dia = tallerConCupo.Taller.Dia,
            HoraInicio = tallerConCupo.Taller.HoraInicio,
            HoraFin = tallerConCupo.Taller.HoraFin,
            Cupo = tallerConCupo.Taller.Cupo,
            CuposDisponibles = tallerConCupo.CuposDisponibles
        };
    }
}
