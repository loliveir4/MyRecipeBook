using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.Users;

namespace MyRecipeBook.Infrastructure.DataAccess.Repositories;

public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
{
    private readonly MyRecipeBookDbCobtext _dbcontext;

    public UserRepository(MyRecipeBookDbCobtext dbcontext) => _dbcontext = dbcontext;

    public async Task Add(User user) => await _dbcontext.Users.AddAsync(user);

    public async Task<bool> ExistActiveUserWithEmail(string email) => await _dbcontext.Users.AnyAsync(u => u.Email == email && u.Active);

}
