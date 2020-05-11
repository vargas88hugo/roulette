using System;
using RouletteApi.Models.Entities;

namespace RouletteApi.Helpers
{
  public class UserHelper
  {
    public static bool existUser(User user)
    {
      if (user == null)
        return false;
      return true;
    }

    public static void CheckUser(User user, string id, int money)
    {
      if (!UserHelper.existUser(user))
        throw new Exception($"The user with id {id} not found");
      if (user.Money < money)
        throw new Exception("User doesn't have enough money");
      user.Money = user.Money - money;
    }
  }
}