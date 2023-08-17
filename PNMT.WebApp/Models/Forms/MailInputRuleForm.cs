using System.ComponentModel.DataAnnotations;

namespace PNMT.WebApp.Models.Forms
{
    public class MailInputRuleForm
    {
        public Guid Id { get; set; }

        public bool Enabled { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        public string? BodyTest { get; set; }

        public string? FromTest { get; set; }

        public string? SubjectTest { get; set; }

        public string? ExtractMessageRegex { get; set; }

        public int? OkCode { get; set; }

        public string? OkTest { get; set; }

        public int? FailCode { get; set; }

        public string? FailTest { get; set; }

        [Required]
        public int DefaultCode { get; set; }

        public Guid? SensorOutputId { get; set; }
    }
}
