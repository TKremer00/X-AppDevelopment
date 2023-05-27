namespace Friendsbook.Core.Validators
{
    internal class HouseNumberValidator : IValidator<string>
    {
        public string ErrorMessage()
        {
            return "A house number must contain numbers";
        }
        public string ErrorMessage(string field)
        {
            return ErrorMessage();
        }

        public bool IsValid(string value)
        {
            if (!value.Any(char.IsDigit))
            {
                return false;
            }

            return true;
        }
    }
}
