using System.ComponentModel.DataAnnotations;

namespace Shared.Application.Validations
{
    public class PasswordValidation : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            string val = value as string;
            if (string.IsNullOrEmpty(val)) return false;
            if (val.Length < 5 || val.Length > 8) return false;
            if (!val.Any(char.IsUpper)) return false;
            if (!val.Any(char.IsLower)) return false;
            if (!val.Any(char.IsNumber)) return false;
            if (val.Any(char.IsWhiteSpace)) return false;
            return true;
        }
    }
}
