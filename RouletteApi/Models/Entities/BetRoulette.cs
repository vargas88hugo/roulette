using System;
using System.ComponentModel.DataAnnotations;

namespace RouletteApi.Models.Entities
{
  public class BetRoulette
  {
    public BetRoulette(BetModel model, string userId)
    {
      Color = model.Color;
      Number = model.Number;
      RouletteId = model.RouletteId;
      UserId = userId;
      Date = DateTime.Now;

    }

    public string Color { get; set; }
    public int Number { get; set; }
    public int Money { get; set; }
    public string RouletteId { get; set; }
    public string UserId { get; set; }
    public DateTime Date { get; set; }
  }
}