using System.ComponentModel.DataAnnotations;

namespace PNMT.WebApp.Models.Forms
{
    public class KeyValueForm
    {
        public Guid Id { get; set; }

        public bool Enabled { get; set; } = true;

        public string Note { get; set; } = "";

        [Required(AllowEmptyStrings = false)]
        public string Key { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Value { get; set; }

        public bool IsReadOnly { get; set; } = false;
    }
}
