using System.ComponentModel.DataAnnotations;

namespace RouletteAPI.Models
{
  public class BetRoulette
  {
    [Required]
    public string Color { get; set; }
    [Required]
    public int Money { get; set; }
    [Required]
    public int Bet { get; set; }
    [Required]
    public string RouletteId { get; set; }
    public string UserId { get; set; }
  }
}