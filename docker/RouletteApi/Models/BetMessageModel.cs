using System.Collections.Generic;
using RouletteApi.Models.Entities;

namespace RouletteApi.Models
{
  public class BetMessageModel
  {
    public readonly string message;
    public readonly List<BetRoulette> bets;

    public BetMessageModel(string message, List<BetRoulette> bets)
    {
      this.message = message;
      this.bets = bets;
    }
  }
}