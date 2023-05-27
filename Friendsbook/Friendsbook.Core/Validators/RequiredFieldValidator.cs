namespace Friendsbook.Core.Validators
{
    internal class RequiredFieldValidator<T> : IValidator<T> where T : class
    {
        public string ErrorMessage()
        {
            return ErrorMessage("This field");
        }

        public string ErrorMessage(string field)
        {
            return $"{field} is required and can't be empty";
        }

        public bool IsValid(T value)
        {
            if (value is string stringValue)
            {
                return !string.IsNullOrEmpty(stringValue);
            }

            return value != null;
        }
    }
}
