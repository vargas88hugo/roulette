using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RouletteApi.Interfaces;
using RouletteApi.Models;

namespace RouletteApi.Controllers
{
  [Route("api/v2/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService) =>
      _authService = authService;

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterModel model)
    {
      try
      {
        await _authService.Register(model);
      }
      catch (System.Exception)
      {
        throw;
      }
      return this.StatusCode(
        StatusCodes.Status201Created,
        $"User with name {model.Username} has been created"
      );
    }
  }
}