using System.Threading.Tasks;
using AutoMapper;
using RouletteApi.Helpers;
using RouletteApi.Interfaces;
using RouletteApi.Models;
using RouletteApi.Models.Entities;

namespace RouletteApi.Services
{
  public class AuthService : IAuthService
  {
    private readonly IUserRepository _userRepository;

    private readonly IMapper _mapper;

    public AuthService(
      IUserRepository userRepository,
      IMapper mapper
    )
    {
      _userRepository = userRepository;
      _mapper = mapper;
    }

    public async Task<User> Register(RegisterModel register)
    {
      var unsetUser = _mapper.Map<User>(register);
      var user = AuthHelper.SetPasswordUser(unsetUser, register.Password);
      return await _userRepository.InsertUser(user);
    }
  }
}