namespace MyRecipeBook.Domain.Repositories.Users;

public interface IUserReadOnlyRepository
{
    public Task<bool>ExistActiveUserWithEmail(string email);
}
