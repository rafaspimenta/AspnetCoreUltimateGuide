using _2.ECommerceOrdersApp.Models;
using System.ComponentModel.DataAnnotations;

namespace _2.ECommerceOrdersApp.CustomValidations;

public class InvoicePriceValidatorAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return null;
        }

        var order = (Order)validationContext.ObjectInstance;
        if (order == null)
        {
            return null;
        }

        //calculate total price
        double totalPrice = 0;
        foreach (Product product in order.Products)
        {
            totalPrice += product.Price * product.Quantity;
        }

        if (totalPrice != (double)value) 
        {
            return new ValidationResult("InvoicePrice doesn't match with the total cost of the specified products in the order.");
        }

        return ValidationResult.Success;
    }
}
