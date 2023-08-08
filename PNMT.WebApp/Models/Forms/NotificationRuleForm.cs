using PNMTD.Lib.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace PNMT.WebApp.Models.Forms
{
    public class NotificationRuleForm
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Recipient { get; set; }

        public bool Enabled { get; set; }

        public NotificationRuleType Type { get; set; }

    }
}
