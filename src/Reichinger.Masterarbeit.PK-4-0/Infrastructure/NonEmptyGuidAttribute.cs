using System;
using System.ComponentModel.DataAnnotations;

namespace Reichinger.Masterarbeit.PK_4_0.Infrastructure
{
    public class NonEmptyGuidAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return value.Equals(Guid.Empty) ? new ValidationResult($"No valid Guid given for field {validationContext.DisplayName}") : null;
        }
    }
}