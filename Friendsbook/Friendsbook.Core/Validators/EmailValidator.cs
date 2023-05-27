using System.Text.RegularExpressions;

namespace Friendsbook.Core.Validators
{
    public class EmailValidator : IValidator<string>
    {
        public bool IsValid(string value)
        {
            return Regex.IsMatch(value, @"([a - zA - Z{ 0 - 9}]+@\s *[a - zA - Z{ 0 - 9}]+)(.nl |.com)$");
        }

        public string ErrorMessage()
        {
            return "An email must contain and @ with text before and after and must end with .nl or .com";
        }

        public string ErrorMessage(string field)
        {
            return ErrorMessage();
        }
    }
}
