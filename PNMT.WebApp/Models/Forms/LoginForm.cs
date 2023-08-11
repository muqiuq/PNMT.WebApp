using System.ComponentModel.DataAnnotations;

namespace PNMT.WebApp.Models.Forms
{
  public class LoginForm
  {

    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

  }
}
