using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RouletteAPI.Interfaces;
using RouletteAPI.Models;
using System.Collections.Generic;

namespace RouletteAPI.Controllers
{
  [Route("api/v1/[controller]")]
  [ApiController]
  public class RoulettesController : ControllerBase
  {
    private readonly IRouletteRepository _rouletteRepository;

    public RoulettesController(IRouletteRepository rouletteRepository)
    {
      _rouletteRepository = rouletteRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<Roulette>> Get() =>
      await _rouletteRepository.GetAllRoulettes();
  }
}