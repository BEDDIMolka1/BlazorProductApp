using BlazorProductApp.Models;
using System.Net.Http.Json;

namespace BlazorProductApp.Services;

public class ProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Product>> GetAll(string? search = null)
    {
        var url = "api/Products";

        if (!string.IsNullOrWhiteSpace(search))
        {
            url += $"?search={Uri.EscapeDataString(search)}";
        }

        Console.WriteLine($"URL APPELEE = {_httpClient.BaseAddress}{url}");

        var result = await _httpClient.GetFromJsonAsync<List<Product>>(url);

        Console.WriteLine("REPONSE RECUE");

        return result ?? new List<Product>();
    }
    public async Task<Product?> GetById(int id)
    {
        return await _httpClient.GetFromJsonAsync<Product>($"api/Products/{id}");
    }

    public async Task<Product?> Create(Product product)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Products", product);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<Product>();
    }

    public async Task<bool> Update(Product product)
    {
        var response = await _httpClient.PutAsJsonAsync(
            $"api/Products/{product.Id}", product);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> Delete(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/Products/{id}");

        return response.IsSuccessStatusCode;
    }
  
}