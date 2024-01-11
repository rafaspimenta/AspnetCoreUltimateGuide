using ModelValidationsExample.CustomValidations;
using System.ComponentModel.DataAnnotations;
namespace ModelValidationsExample.Models;

public class Person : IValidatableObject
{
    [Required]
    public string? PersonName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
    public double? Price { get; set; }

    [MinimumYearValidator(MinimumYear = 2001, ErrorMessage = "Minimum year allowed is {0}")]
    public DateTime? DateOfBirth { get; set; }
    public DateTime? Age { get; set; }

    public override string ToString()
    {
        return $"Person object - Name: {PersonName}, Email: {Email}, Phone: {Phone}, Password: {Password}, Confirm Password: {ConfirmPassword}, Price: {Price}";
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!DateOfBirth.HasValue && !Age.HasValue)
        {
            yield return new ValidationResult("Either of Date of Birth or Age must be supplied", new[] { nameof(Age) });
        }
    }
}