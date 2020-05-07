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

    [HttpGet("{id:length(24)}", Name = "GetUser")]
    public async Task<IActionResult> Get(string id)
    {
      User user = await _userRepository.GetUser(id);
      if (user == null)
      {
        return NotFound();
      }
      return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Post(User user)
    {
      User newUser = await _userRepository.CreateUser(user);
      if (newUser == null)
      {
        return BadRequest();
      }
      return CreatedAtRoute("Get",
        new { id = user.Id.ToString() },
        new { message = $"User with id {user.Id} has been created" });
    }
  }
}