using System;
using System.Collections.Generic;
using RouletteApi.Models.Entities;

namespace RouletteApi.Helpers
{
  public class RouletteHelper
  {
    private static bool isRouletteOpen(string status)
    {
      if (status != "Open")
        return false;
      return true;
    }

    public static bool existRoulette(Roulette roulette)
    {
      if (roulette == null)
        return false;
      return true;
    }

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

    public static void CheckBetRoulette(Roulette roulette, string rouletteId)
    {
      if (!RouletteHelper.existRoulette(roulette))
        throw new Exception($"Roulette with id {rouletteId} not found");
      if (!RouletteHelper.isRouletteOpen(roulette.Status))
        throw new Exception($"The roulette with id {rouletteId} is closed");
    }

    public static void ConfigureOpenRoulette(Roulette roulette, string rouletteId)
    {
      if (!RouletteHelper.existRoulette(roulette))
        throw new Exception($"Roulette with id {rouletteId} not found");
      roulette.Status = "Open";
      roulette.Bets = new List<BetRoulette>();
    }
  }
}