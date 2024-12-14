using ExemploArquiteturaAtual.Infra;
using ExemploArquiteturaAtual.Models;
using Microsoft.EntityFrameworkCore;

namespace ExemploArquiteturaAtual.Repositories;
public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;
    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateUserAsync(User userRequest)
    {
        await _dbContext.Users.AddAsync(userRequest);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<User>> ReturnActiveUsersAsync()
    {
        return await _dbContext.Users
                     .Where(user => user.Active)
                     .ToListAsync();
    }

    public async Task<User> ReturnUserByEmailAsync(string email)
    {
        return await _dbContext
                     .Users
                     .SingleOrDefaultAsync(user => user.Email == email) ?? new User();
    }
}

