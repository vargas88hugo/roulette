using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RouletteApi.Interfaces;
using RouletteApi.Models.Entities;

namespace RouletteApi.Controllers
{
  [Route("api/v2/[controller]")]
  public class UserController : ControllerBase
  {
    private readonly IUserService _userService;

    public UserController(IUserService userService) =>
      _userService = userService;

    [HttpGet]
    public async Task<IEnumerable<User>> GetAllUsers() =>
      await _userService.GetAllUsers();
  }
}