namespace RouletteApi.Models
{
  public class RouletteDatabaseSettings : IRouletteDatabaseSettings
  {
    public string RoulletesCollectionName { get; set; }
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
  }

  public interface IRouletteDatabaseSettings
  {
    string RoulletesCollectionName { get; set; }
    string ConnectionString { get; set; }
    string DatabaseName { get; set; }
  }
}