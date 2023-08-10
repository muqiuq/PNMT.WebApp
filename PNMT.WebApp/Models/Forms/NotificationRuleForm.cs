using PNMTD.Lib.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace PNMT.WebApp.Models.Forms
{
    public class NotificationRuleForm
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Recipient { get; set; }

        public bool Enabled { get; set; }

        [Required]
        public NotificationRuleType Type { get; set; }

    }
}
