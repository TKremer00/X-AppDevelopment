namespace Friendsbook.Core.Responses
{
    public class ErrorResponse : IResponse
    {
        public ErrorResponse(string[] errorMessages)
        {
            ErrorMessages = errorMessages;
        }

        public bool IsSuccess => false;

        public string[] ErrorMessages { get; }
    }
}
