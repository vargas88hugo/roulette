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
    private readonly IRouletteService _rouletteService;

    public RouletteController(IRouletteService rouletteService) =>
      _rouletteService = rouletteService;

    [HttpGet]
    public async Task<IEnumerable<Roulette>> GetAllRoulettes() =>
      await _rouletteService.GetAllRoulettes();

    [HttpGet("{id:length(24)}")]
    public async Task<Roulette> GetRoulette(string id) =>
      await _rouletteService.GetRoulette(id);
  }
}