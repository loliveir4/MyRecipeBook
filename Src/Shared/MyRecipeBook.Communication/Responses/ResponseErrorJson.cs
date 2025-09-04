namespace MyRecipeBook.Communication.Responses;

public class ResponseErrorJson
{
    public IList<string> ErrorMessages { get; set; }

    public ResponseErrorJson(IList<string> errorMessages) => ErrorMessages = errorMessages;
    public ResponseErrorJson(string errorMessage)
    {
        ErrorMessages = [errorMessage];
    }

}
