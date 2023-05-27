namespace Friendsbook.Core.Validators
{
    internal class NoDigitsInStringValidator : IValidator<string>
    {
        public string ErrorMessage()
        {
            return ErrorMessage("This field");
        }

        public string ErrorMessage(string field)
        {
            return $"{field} can't contain any numbers";
        }

        public bool IsValid(string value)
        {
            return !value.Any(char.IsDigit);
        }
    }
}
