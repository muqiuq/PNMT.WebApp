using System.ComponentModel.DataAnnotations;

namespace PNMT.WebApp.Models.Forms
{
    public class MailInputForm
    {
        public Guid Id { get; set; }

        public bool Enabled { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        public string? ContentTest { get; set; }

        public string? SenderTest { get; set; }

        public int? OkCode { get; set; }

        public string? OkTest { get; set; }

        public int? FailCode { get; set; }

        public string? FailTest { get; set; }

        [Required]
        public int DefaultCode { get; set; }

        [Required]
        public Guid? SensorOutputId { get; set; }
    }
}
