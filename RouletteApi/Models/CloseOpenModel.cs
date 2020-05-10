using System.ComponentModel.DataAnnotations;

namespace RouletteApi.Models
{
  public class CloseOpenModel
  {
    [Required(ErrorMessage = "Roulette Id is obligatory")]
    public string RouletteId { get; set; }
  }
}