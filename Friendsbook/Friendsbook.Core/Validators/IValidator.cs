namespace Friendsbook.Core.Validators
{
    public interface IValidator<T>
    {
        bool IsValid(T value);

        string ErrorMessage();

        string ErrorMessage(string field);
    }
}
