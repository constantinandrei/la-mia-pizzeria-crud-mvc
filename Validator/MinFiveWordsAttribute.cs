using System.ComponentModel.DataAnnotations;

namespace il_mio_primo_blog.Validator
{
    public class MinFiveWordsAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string fieldValue = (string)value;

            if (fieldValue != null && fieldValue.Trim().Count(c => c == ' ') < 4)
            {
                return new ValidationResult("Il campo deve contenere almeno cinque parole");
            }

            return ValidationResult.Success;
        }
    }
}
