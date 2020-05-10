using System.ComponentModel.DataAnnotations;

namespace RouletteApi.Models
{
  public class BetModel
  {
    [Required(ErrorMessage = "Color is obligatory")]
    public string Color { get; set; }
    [Required(ErrorMessage = "Money is obligatory")]
    public int Money { get; set; }
    [Required(ErrorMessage = "Number is obligatory")]
    public int Number { get; set; }
    [Required(ErrorMessage = "Roulette Id is obligatory")]
    public string RouletteId { get; set; }
  }
}