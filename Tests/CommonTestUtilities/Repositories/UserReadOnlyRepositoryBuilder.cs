using Moq;
using MyRecipeBook.Domain.Repositories.Users;

namespace CommonTestUtilities.Repositories;

public class UserReadOnlyRepositoryBuilder
{
    private readonly Mock<IUserReadOnlyRepository> _repository;

    public UserReadOnlyRepositoryBuilder() => _repository = new Mock<IUserReadOnlyRepository>();
   
    public IUserReadOnlyRepository Build() => _repository.Object;

    public void ExistActiveUserWithEmail(string email) => _repository.Setup(repostory => repostory.ExistActiveUserWithEmail(email)).ReturnsAsync(true);
}
