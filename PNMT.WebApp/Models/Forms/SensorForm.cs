using PNMTD.Lib.Models.Enum;

namespace PNMT.WebApp.Models.Forms
{
    public class SensorForm
    {
        public Guid Id { get; set; }

        public SensorType Type { get; set; }

        public string Name { get; set; }

        public string? TextId { get; set; }

        public bool Enabled { get; set; }

        public int Interval { get; set; }

        public int GracePeriod { get; set; }
    }
}
