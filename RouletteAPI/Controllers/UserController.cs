using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RouletteAPI.Interfaces;
using RouletteAPI.Models;
using System.Collections.Generic;

namespace RouletteAPI.Controllers
{
  [Route("api/v1/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
      _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<User>> Get() =>
      await _userRepository.GetAllUsers();
  }
}