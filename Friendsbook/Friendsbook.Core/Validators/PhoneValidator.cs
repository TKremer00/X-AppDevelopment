using System.Text.RegularExpressions;

namespace Friendsbook.Core.Validators
{
    internal class PhoneValidator : IValidator<string>
    {
        public string ErrorMessage()
        {
            return "A phone number can only contain numbers";
        }

        public string ErrorMessage(string field)
        {
            return ErrorMessage();
        }

        public bool IsValid(string value)
        {
            return Regex.IsMatch(value, @"^\+?[0-9]{10,11}$");
        }
    }
}
