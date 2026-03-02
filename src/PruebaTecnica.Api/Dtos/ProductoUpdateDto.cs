using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.Api.Dtos;

public class ProductoUpdateDto
{
    [Required, StringLength(20)]
    public string Codigo { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Required, Range(0.01, double.MaxValue)]
    public decimal Precio { get; set; }

    [Required, Range(0, int.MaxValue)]
    public int Stock { get; set; }

    public bool Activo { get; set; } = true;
}