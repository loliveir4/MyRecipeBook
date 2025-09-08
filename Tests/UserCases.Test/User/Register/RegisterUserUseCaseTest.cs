using Bogus;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request;
using FluentAssertions;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace UserCases.Test.User.Register;

public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var userCase = CreateUseCase();
        var result = await userCase.Execute(request);

        result.Should().NotBeNull();
        result.Name.Should().Be(request.Name);
    }

    [Fact]
    public async Task Error_Email_Already_Registered()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var userCase = CreateUseCase(request.Email);

        Func<Task> userCaseAction = async () => await userCase.Execute(request);

       (await userCaseAction.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(e => e.ErrorMessages.Count == 1 && e.ErrorMessages.Contains(ResourceMessagesException.EMAIL_ALREADY_EXIST));

    }

    [Fact]
    public async Task Error_Name_Empty()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;

        var userCase = CreateUseCase();

        Func<Task> userCaseAction = async () => await userCase.Execute(request);

        (await userCaseAction.Should().ThrowAsync<ErrorOnValidationException>())
             .Where(e => e.ErrorMessages.Count == 1 && e.ErrorMessages.Contains(ResourceMessagesException.NAME_EMPTY));

    }

    private static RegisterUserUseCase CreateUseCase(string? email = null)
    {

        var mapper = MapperBuilder.Build();
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var userUnit = UnitOfWorkBuilder.Build();
        var writeRepository = UserWriteOnlyRepositoryBuilder.Build();
        var readOnlyRepositoryBuilder = new UserReadOnlyRepositoryBuilder();

        if(string.IsNullOrEmpty(email) == false)
         readOnlyRepositoryBuilder.ExistActiveUserWithEmail(email); 

        return new RegisterUserUseCase(writeRepository, readOnlyRepositoryBuilder.Build(), mapper, passwordEncripter, userUnit);

    }
}

