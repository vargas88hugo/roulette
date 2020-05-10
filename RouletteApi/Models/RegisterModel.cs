using System.ComponentModel.DataAnnotations;

namespace RouletteApi.Models
{
  public class RegisterModel
  {
    [
      Required(ErrorMessage = "Username is obligatory"),
      MaxLength(25, ErrorMessage = "No more than 25 characters")
    ]
    public string Username { get; set; }
    [
      Required(ErrorMessage = "Password is obligatory")
    ]
    public string Password { get; set; }
    [
      Required(ErrorMessage = "Money is obligatory"),
      Range(1, int.MaxValue, ErrorMessage = "Please enter a money bigger than 1")
    ]
    public int Money { get; set; }
  }
}