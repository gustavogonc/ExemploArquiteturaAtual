using ExemploArquiteturaAtual.Models;

namespace ExemploArquiteturaAtual.Business.Interfaces;
public interface IUserBusiness
{
    Task RegisterNewUserAsync(RegisterUserDTO userRequest);
    Task<List<ResponseUserDTO>> RecoverActiveUsersAsync();
}

