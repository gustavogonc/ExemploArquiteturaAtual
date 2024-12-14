using ExemploArquiteturaAtual.Business.Interfaces;
using ExemploArquiteturaAtual.Models;
using ExemploArquiteturaAtual.Repositories;
using ExemploArquiteturaAtual.Services;

namespace ExemploArquiteturaAtual.Business.Auth;
public class AuthenticationBusiness : IAuthenticationBusiness
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    public AuthenticationBusiness(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }
    public async Task<ResponseUserDTO> LoginAsync(LoginRequestDTO loginRequest)
    {
        User user = await _userRepository.ReturnUserByEmailAsync(loginRequest.Email);
        var validHours = _configuration.GetValue<int>("JwtConfiguration:ValidHours");
        var issuer = _configuration.GetValue<string>("JwtConfiguration:Issuer");

        if (user is null)
        {
            return null;
        }

        bool passwordIsValid = BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password);

        if (!passwordIsValid)
        {
            return null;
        }

        var token = TokenService.GenerateToken(user.Email, user.Name, issuer!, validHours);

        return new ResponseUserDTO(Name: user.Name, Email: user.Email, Token: token);
    }
}

