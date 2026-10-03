using System.ComponentModel.DataAnnotations;

namespace BlazorProductApp.Models;

public class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Le nom est obligatoire.")]
    [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères.")]
    public string Name { get; set; } = "";

    [Range(0.01, 999999.99, ErrorMessage = "Le prix doit être supérieur à 0.")]
    public decimal Price { get; set; }
}