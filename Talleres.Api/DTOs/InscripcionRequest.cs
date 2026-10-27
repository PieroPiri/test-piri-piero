using System.ComponentModel.DataAnnotations;

namespace Talleres.Api.DTOs;

public class InscripcionRequest
{
    [Required(ErrorMessage = "El TallerId es obligatorio.")]
    [Range(1, int.MaxValue, ErrorMessage = "El TallerId debe ser positivo.")]
    public int? TallerId { get; set; }

    [Required(ErrorMessage = "El DNI es obligatorio.")]
    [Range(1, int.MaxValue, ErrorMessage = "El DNI debe ser positivo.")]
    public int? Dni { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "El nombre del alumno es obligatorio.")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "El nombre del alumno no puede estar vacío.")]
    public string NombreAlumno { get; set; } = string.Empty;
}
