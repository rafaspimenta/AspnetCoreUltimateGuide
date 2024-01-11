using System.ComponentModel.DataAnnotations;

namespace ModelValidationsExample.CustomValidations;

public class MinimumYearValidatorAttribute : ValidationAttribute
{
    public int MinimumYear { get; set; } = 2000;
    public string DefaultErrorMessage { get; set; } = "Minimum year allowed is {0}";
    public MinimumYearValidatorAttribute()
    {
    }
    public MinimumYearValidatorAttribute(int minimumYear)
    {
        MinimumYear = minimumYear;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        DateTime? date = (DateTime?)value;
        if (!date.HasValue)
        {
            return null;
        }

        if (date.Value.Year <= MinimumYear)
        {
            return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage, MinimumYear));
        }

        return ValidationResult.Success;
    }
}
