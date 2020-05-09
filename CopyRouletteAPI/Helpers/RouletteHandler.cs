using System;
using RouletteAPI.Models;

namespace RouletteAPI.Helpers
{
  public class RouletteHandler
  {
    public static string ChooseWinningBet(Roulette roulette)
    {
      Random random = new Random();
      int number = random.Next(37);
      string color = random.Next(2) == 1 ? "black" : "red";
      foreach (BetRoulette bet in roulette.Bets)
      {
        if (bet.Color.ToLower().Equals(color) && bet.Number == number)
          return $"color {color} number {number} | The Bet with user id {bet.UserId} is the winner!!";
      }
      return $"Result: color {color} number {number} | There is not winner";
    }
  }
}