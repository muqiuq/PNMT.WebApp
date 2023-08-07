using System.ComponentModel.DataAnnotations;

namespace PNMT.WebApp.Models.Forms
{
    public class HostForm
    {

        public Guid Id { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 2)]
        public string? Location { get; set; }

        public string? Notes { get; set; }

        public DateTime Created { get; set; }

        [Required]
        public bool Enabled { get; set; }

    }
}
