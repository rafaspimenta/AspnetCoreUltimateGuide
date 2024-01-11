using _2.ECommerceOrdersApp.CustomValidations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace _2.ECommerceOrdersApp.Models;

public class Order
{
    private Random _random = new();
    public Order()
    {
        OrderNo = _random.Next(1, 99999);
    }
    
    public int? OrderNo { get; set; }

    [Required]
    public DateTime? OrderDate { get; set; }

    [InvoicePriceValidator]
    public double InvoicePrice { get; set; }

    [MinLength(1)]
    public IList<Product> Products { get; set; } = new List<Product>();
}
