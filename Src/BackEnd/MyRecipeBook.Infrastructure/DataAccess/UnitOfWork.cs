using MyRecipeBook.Domain.Entities;

namespace MyRecipeBook.Infrastructure.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private readonly MyRecipeBookDbCobtext _dbcontext;

    public UnitOfWork(MyRecipeBookDbCobtext dbcontext) => _dbcontext = dbcontext;

    public async Task Commit() => await _dbcontext.SaveChangesAsync();
}
