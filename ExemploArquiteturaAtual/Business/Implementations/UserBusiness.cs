using ExemploArquiteturaAtual.Business.Interfaces;
using ExemploArquiteturaAtual.Models;
using ExemploArquiteturaAtual.Repositories;

namespace ExemploArquiteturaAtual.Business.Implementations;
public class UserBusiness : IUserBusiness
{
    private readonly IUserRepository _userRepository;

    public UserBusiness(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<ResponseUserDTO>> RecoverActiveUsersAsync()
    {
        List<ResponseUserDTO> listResponse = [];
        List<User> resultList = await _userRepository.ReturnActiveUsersAsync();

        foreach (var user in resultList)
        {
            ResponseUserDTO userResponse = new(Email: user.Email, Name: user.Name);

            listResponse.Add(userResponse);
        }

        return listResponse;
    }

    public async Task RegisterNewUserAsync(RegisterUserDTO userRequest)
    {

        if (string.IsNullOrWhiteSpace(userRequest.Email) || string.IsNullOrWhiteSpace(userRequest.Password))
        {
            throw new InvalidDataException("Invalid e-mail and/or password data");
        }

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userRequest.Password);

        User user = new()
        {
            Name = userRequest.Name,
            Email = userRequest.Email,
            Password = hashedPassword,
        };

        await _userRepository.CreateUserAsync(user);
    }
}

