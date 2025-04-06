using System.ComponentModel.DataAnnotations;

namespace PNMT.WebApp.Models.Forms.Helpers
{
    public static class PasswordValidationHelper
    {
        public static IEnumerable<ValidationResult> ValidatePassword(
            string password,
            string repeatPassword,
            string passwordFieldName = "Password",
            string repeatFieldName = "RepeatPassword")
        {
            if (string.IsNullOrEmpty(password) || password.Length < 10)
            {
                yield return new ValidationResult(
                    "Password must be at least 10 characters long.",
                    new[] { passwordFieldName });
            }

            if (password != repeatPassword)
            {
                yield return new ValidationResult(
                    "Passwords do not match.",
                    new[] { repeatFieldName });
            }
        }
    }

}
