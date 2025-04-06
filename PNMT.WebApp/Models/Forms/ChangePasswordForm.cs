using PNMT.WebApp.Models.Forms.Helpers;
using System.ComponentModel.DataAnnotations;

namespace PNMT.WebApp.Models.Forms
{
    public class ChangePasswordForm : IValidatableObject
    {
        [Required]
        public string NewPassword { get; set; }

        [Required]
        public string RepeatNewPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return PasswordValidationHelper.ValidatePassword(NewPassword, RepeatNewPassword, nameof(NewPassword), nameof(RepeatNewPassword));
        }
    }
}
