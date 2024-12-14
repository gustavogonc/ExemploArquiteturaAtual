using ExemploArquiteturaAtual.Models;

namespace ExemploArquiteturaAtual.Repositories;
public interface IUserRepository
{
    Task CreateUserAsync(User userRequest);
    Task<User> ReturnUserByEmailAsync(string email);
    Task<List<User>> ReturnActiveUsersAsync();
}

