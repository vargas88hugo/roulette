using System.ComponentModel.DataAnnotations;

namespace RouletteApi.Models
{
  public class AuthenticateModel
  {
    [Required(ErrorMessage = "Username is obligatory")]
    [MaxLength(25, ErrorMessage = "No more than 25 characters")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is obligatory")]
    [MaxLength(25, ErrorMessage = "No more than 25 characters")]
    public string Password { get; set; }
  }
}