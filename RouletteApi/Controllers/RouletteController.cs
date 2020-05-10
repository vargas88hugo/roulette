using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouletteApi.Interfaces;
using RouletteApi.Models.Entities;

namespace RouletteApi.Controllers
{
  [Authorize]
  [Route("api/v2/[controller]")]
  [ApiController]
  public class RouletteController : ControllerBase
  {
    private readonly IRouletteRepository _rouletteRepository;

    public RouletteController(IRouletteRepository rouletteRepository) =>
      _rouletteRepository = rouletteRepository;

    public async Task<IEnumerable<Roulette>> GetAllRoulettes() =>
      await _rouletteRepository.GetAllRoulettes();
  }
}