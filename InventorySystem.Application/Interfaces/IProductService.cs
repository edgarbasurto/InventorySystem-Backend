using InventorySystem.Application.DTOs;

namespace InventorySystem.Application.Interfaces;

public interface IProductService
{
    Task<int> AddProductAsync(ProductDto productDto);
    Task<PagedResultDto<ProductDto>> GetProductsAsync(int page, int size, string sortBy, string search);
    Task<ProductDto> GetProductByIdAsync(int id);
    Task<bool> UpdateProductAsync(int id, ProductDto productDto);
    Task<bool> DeleteProductAsync(int id);
}