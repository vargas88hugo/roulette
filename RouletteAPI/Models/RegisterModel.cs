using System.ComponentModel.DataAnnotations;

namespace RouletteAPI.Models
{
  public class RegisterModel
  {
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
  }
}