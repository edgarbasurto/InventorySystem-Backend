using InventorySystem.Application.DTOs;
using InventorySystem.Application.Interfaces;
using InventorySystem.Domain.Entities;
using InventorySystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Application.Services;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;

    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddProductAsync(ProductDto productDto)
    {
        var product = new Product
        {
            Name = productDto.Name,
            Price = productDto.Price,
            Description = productDto.Description,
            Stock = productDto.Stock,
            Status = productDto.Status,
            ImageUrl = productDto.ImageUrl,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        return product.Id;
    }
    
    public async Task<PagedResultDto<ProductDto>> GetProductsAsync(int page, int size, string sortBy, string search)
    {
        var query = _context.Products.AsQueryable();
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p =>
                p.Name.Contains(search) ||
                p.Description.Contains(search) ||
                p.Price.ToString().Contains(search));
        }
  
        query = sortBy.ToLower() switch
        {
            "price" => query.OrderBy(p => p.Price),
            "date" => query.OrderByDescending(p => p.CreatedAt),
            "name" => query.OrderBy(p => p.Name),
            _ => query.OrderBy(p => p.Name),
        };

        var totalRecords = await query.CountAsync();
        
        var products = await query
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync();
        
        var productDtos = products.Select(p => new ProductDto
        {
            Name = p.Name,
            Price = p.Price,
            Description = p.Description,
            Stock = p.Stock,
            Status = p.Status,
            ImageUrl = p.ImageUrl
        }).ToList();
        
        return new PagedResultDto<ProductDto>
        {
            TotalRecords = totalRecords,
            Items = productDtos
        };
    }
    public async Task<ProductDto> GetProductByIdAsync(int id)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
            return null;

        return new ProductDto
        {
            Name = product.Name,
            Price = product.Price,
            Description = product.Description,
            Stock = product.Stock,
            Status = product.Status,
            ImageUrl = product.ImageUrl
        };
    }
    
    public async Task<bool> UpdateProductAsync(int id, ProductDto productDto)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
            return false;

        // Actualizar los campos del producto
        product.Name = productDto.Name;
        product.Price = productDto.Price;
        product.Description = productDto.Description;
        product.Stock = productDto.Stock;
        product.Status = productDto.Status;
        product.ImageUrl = productDto.ImageUrl;
        product.UpdatedAt = DateTime.UtcNow;

        _context.Products.Update(product);
        await _context.SaveChangesAsync();

        return true;
    }
    
    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
            return false;

        // Marcar el producto como eliminado
        product.IsActive = false;
        product.UpdatedAt = DateTime.UtcNow;

        _context.Products.Update(product);
        await _context.SaveChangesAsync();

        return true;
    }
    
}