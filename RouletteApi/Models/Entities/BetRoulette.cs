using System;
using System.ComponentModel.DataAnnotations;

namespace RouletteApi.Models.Entities
{
  public class BetRoulette
  {
    BetRoulette() =>
      Date = DateTime.Now;

    [Required]
    public string Color { get; set; }
    [Required]
    public int Number { get; set; }
    [Required]
    public int Money { get; set; }
    [Required]
    public string RouletteId { get; set; }
    public string UserId { get; set; }
    public DateTime Date { get; set; }
  }
}