using System.Globalization;

namespace MyRecipeBook.Api.MiddleWare;

public class CultureMiddleware
{
    private readonly RequestDelegate _next;
    public CultureMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        var supportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures);

        var acceptLanguageHeader = context.Request.Headers["Accept-Language"].ToString();
        var requestedCulture = acceptLanguageHeader?.Split(',').FirstOrDefault();


        var culture = new CultureInfo("en");

        if (string.IsNullOrWhiteSpace(requestedCulture) == false && supportedLanguages.Any(c => c.Name.Equals(requestedCulture)))
        {

            culture = new CultureInfo(requestedCulture);

        }

        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;

        await _next(context);
    }
}
