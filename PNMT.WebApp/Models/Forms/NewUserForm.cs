using PNMT.WebApp.Models.Forms.Helpers;
using System.ComponentModel.DataAnnotations;

namespace PNMT.WebApp.Models.Forms
{
    public class NewUserForm : IValidatableObject
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsAdmin { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string RepeatPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return PasswordValidationHelper.ValidatePassword(Password, RepeatPassword);
        }
    }
}
