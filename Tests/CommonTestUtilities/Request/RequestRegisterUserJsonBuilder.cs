using Bogus;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Entities;

namespace CommonTestUtilities.Request;

public class RequestRegisterUserJsonBuilder
{
    public static RequestRegisterUserjson Build(int passwordLength = 10)
    {
        return new Faker<RequestRegisterUserjson>()
         .RuleFor(u => u.Name, f => f.Person.FirstName)
         .RuleFor(u => u.Password, f => f.Internet.Password(passwordLength))
         .RuleFor(u => u.Email, (f, user) => f.Internet.Email(user.Name));
    }
}
