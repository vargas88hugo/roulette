namespace RouletteApi.Models
{
  public class MakeBetMessageModel
  {
    public readonly string message;

    public MakeBetMessageModel(string message) =>
      this.message = message;
  }
}