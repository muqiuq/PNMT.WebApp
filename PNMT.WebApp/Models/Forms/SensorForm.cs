using PNMTD.Lib.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace PNMT.WebApp.Models.Forms
{
    public class SensorForm
    {
        public Guid Id { get; set; }

        [Required]
        public SensorType Type { get; set; }

        [Required]
        public string Name { get; set; }

        public string? TextId { get; set; }

        [Required]
        public bool Enabled { get; set; }

        public bool Ignore { get; set; } = false;

        public int Interval { get; set; }

        public int GracePeriod { get; set; }
    }
}
