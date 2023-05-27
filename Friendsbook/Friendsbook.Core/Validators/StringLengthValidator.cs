namespace Friendsbook.Core.Validators
{
    internal class StringLengthValidator : IValidator<string>
    {
        private readonly int _minumimStringLenght;

        public StringLengthValidator(int minumimStringLenght)
        {
            _minumimStringLenght = minumimStringLenght;
        }

        public string ErrorMessage()
        {
            return ErrorMessage("This field");
        }

        public string ErrorMessage(string field)
        {
            return $"{field} must have more than {_minumimStringLenght} characters";
        }

        public bool IsValid(string value)
        {
            return value.Length >= _minumimStringLenght;
        }
    }
}
