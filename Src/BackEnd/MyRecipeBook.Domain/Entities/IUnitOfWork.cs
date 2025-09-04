namespace MyRecipeBook.Domain.Entities;

public interface IUnitOfWork
{
    public Task Commit();
}
