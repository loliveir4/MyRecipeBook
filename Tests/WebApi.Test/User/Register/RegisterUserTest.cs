using CommonTestUtilities.Request;
using FluentAssertions;
using MyRecipeBook.Exceptions;
using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using WebApi.Test.InLineData;

namespace WebApi.Test.User.Register;

public class RegisterUserTest : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient;
    public RegisterUserTest(CustomWebApplicationFactory factory) => _httpClient = factory.CreateClient();

    [Fact]
    public async Task Sucess()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        var response = await _httpClient.PostAsJsonAsync("User", request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        await using var responsyBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responsyBody);

        responseData.RootElement.GetProperty("name").GetString().Should().NotBeNullOrWhiteSpace().And.Be(request.Name);
    }

    [Theory]
    [ClassData(typeof(CultureInLineDataTest))]
    public async Task Error_Empty_Name(string culture)
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;

        if (_httpClient.DefaultRequestHeaders.Contains("Accept-Language"))
            _httpClient.DefaultRequestHeaders.Remove("Accept-Language");

        _httpClient.DefaultRequestHeaders.Add("Accept-Language", culture);

        var response = await _httpClient.PostAsJsonAsync("User", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responsyBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responsyBody);

        var erros = responseData.RootElement.GetProperty("errorMessages").EnumerateArray();

        var expectedMessage = ResourceMessagesException.ResourceManager.GetString("NAME_EMPTY",new CultureInfo(culture));

        erros.Should().ContainSingle().And.Contain(error => error.GetString()!.Equals(expectedMessage));
    }

}
