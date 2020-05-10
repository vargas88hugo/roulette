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

    public async Task<User> Register(RegisterModel model)
    {
      var unsetUser = _mapper.Map<User>(model);
      var user = AuthHelper.SetPasswordUser(unsetUser, model.Password);
      await _userRepository.InsertUser(user);
      return user;
    }
    public async Task<User> Authenticate(AuthenticateModel model)
    {
      var user = await _userRepository.GetUserByName(model.Username);
      AuthHelper.CheckAuthentication(user, model.Password);
      return user;
    }
  }
}