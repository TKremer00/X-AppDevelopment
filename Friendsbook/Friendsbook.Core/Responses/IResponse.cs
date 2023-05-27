namespace Friendsbook.Core.Responses
{
    public interface IResponse
    {
        public bool IsSuccess { get; }

        public string[] ErrorMessages { get; }
    }
}
