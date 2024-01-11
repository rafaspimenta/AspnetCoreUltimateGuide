using System.ComponentModel.DataAnnotations;

namespace _2.ECommerceOrdersApp.Models;

public class Product
{
    [Required]
    public int ProductCode { get; set; }

    //price per unit
    [Required]
    public double Price { get; set; }

    [Required]
    public int Quantity { get; set; }
}