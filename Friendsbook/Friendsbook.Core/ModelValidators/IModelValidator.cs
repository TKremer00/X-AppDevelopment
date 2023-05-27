namespace Friendsbook.Core.Validators
{
    public interface IModelValidator<T> where T : class
    {
        string[] Validate(T model);
    }
}
