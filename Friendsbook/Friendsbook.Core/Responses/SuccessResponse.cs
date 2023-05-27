namespace Friendsbook.Core.Responses
{
    public class SuccessResponse : IResponse
    {
        public bool IsSuccess => true;

        public string[] ErrorMessages => throw new NotImplementedException();
    }
}
