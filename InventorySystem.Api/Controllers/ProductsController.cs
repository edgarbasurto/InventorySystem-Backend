using InventorySystem.Application.DTOs;
using InventorySystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }
    [HttpGet]
    public async Task<IActionResult> GetProducts(
        [FromQuery] int page = 1,
        [FromQuery] int size = 10,
        [FromQuery] string sortBy = "name",
        [FromQuery] string search = "")
    {
        if (page < 1 || size < 1)
            return BadRequest("Page and size must be greater than 0.");

        var products = await _productService.GetProductsAsync(page, size, sortBy, search);
        return Ok(products);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] ProductDto productDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var productId = await _productService.AddProductAsync(productDto);
        return CreatedAtAction(nameof(GetProductById), new { id = productId }, productDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
            return NotFound($"No se encontró un producto con ID {id}.");

        return Ok(product);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto productDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _productService.UpdateProductAsync(id, productDto);
        if (!updated)
            return NotFound($"No se encontró un producto con ID {id} para actualizar.");

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var deleted = await _productService.DeleteProductAsync(id);
        if (!deleted)
            return NotFound($"No se encontró un producto con ID {id} para eliminar.");

        return NoContent();
    }
    
}