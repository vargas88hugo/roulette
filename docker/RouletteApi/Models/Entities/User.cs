using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RouletteApi.Models.Entities
{
  public class User
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string UserName { get; set; }
    public int Money { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
  }
}