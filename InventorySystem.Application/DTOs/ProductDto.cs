using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Application.DTOs;

public class ProductDto
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string Name { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0.")]
    public decimal Price { get; set; }

    public string Description { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
    public int Stock { get; set; }
    public string Status { get; set; }
    public string ImageUrl { get; set; }
}