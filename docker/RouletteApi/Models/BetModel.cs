using System.ComponentModel.DataAnnotations;

namespace RouletteApi.Models
{
  public class BetModel
  {
    [Required(ErrorMessage = "Color is obligatory")]
    [RegularExpression("^(((b|B)lack)|((r|R)ed))$",
    ErrorMessage = "Please enter a red or black color")]
    public string Color { get; set; }

    [Required(ErrorMessage = "Money is obligatory")]
    [Range(0, 10000, ErrorMessage = "Bet amount must be between 0 and 1000")]
    public int Money { get; set; }

    [Required(ErrorMessage = "Number bet is obligatory")]
    [Range(0, 36, ErrorMessage = "Number bet must be between 0 and 36")]
    public int Number { get; set; }

    [Required(ErrorMessage = "Roulette Id is obligatory")]
    [StringLength(25)]
    public string RouletteId { get; set; }
  }
}