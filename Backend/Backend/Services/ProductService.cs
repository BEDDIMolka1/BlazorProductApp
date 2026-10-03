using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class ProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAll(string? search = null)
    {
        Console.WriteLine($"SEARCH SERVICE = '{search}'");

        var query = _context.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(p => p.Name.Contains(search));
        }

        var result = await query.ToListAsync();

        Console.WriteLine($"RESULT COUNT SERVICE = {result.Count}");

        return result;
    }

    public async Task<Product?> GetById(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<Product> Create(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return product;
    }

    public async Task<Product?> Update(int id, Product product)
    {
        var existingProduct = await _context.Products.FindAsync(id);

        if (existingProduct == null)
            return null;

        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;


        await _context.SaveChangesAsync();

        return existingProduct;
    }

    public async Task<bool> Delete(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
            return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return true;
    }
}