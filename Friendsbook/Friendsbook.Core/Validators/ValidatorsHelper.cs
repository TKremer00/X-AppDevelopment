namespace Friendsbook.Core.Validators
{
    public static class ValidatorsHelper
    {
        public static readonly IValidator<string>[] NameValidators = new IValidator<string>[] {
            new NoDigitsInStringValidator(),
            new StringLengthValidator(2),
        };

        public static readonly IValidator<string> HouseNumberValidator = new HouseNumberValidator();

        public static IValidator<string>[] StringNoDigitLengthValidator(int stringLength) => new IValidator<string>[] {
            new NoDigitsInStringValidator(),
            new StringLengthValidator(stringLength),
        };

        public static readonly IValidator<string> PhoneNumberValidator = new PhoneValidator();

        public static readonly IValidator<string> EmailValidator = new EmailValidator();

        public static string[] Validate<T>(IValidator<T>[] validator, T value, string fieldName)
        {
            return validator.Where(x => !x.IsValid(value)).Select(x => x.ErrorMessage(fieldName)).ToArray();
        }

        public static (bool IsValid, string ErrorMessage) ValidateRequriedField<T>(T value, string field) where T : class
        {
            var validator = new RequiredFieldValidator<T>();
            return (validator.IsValid(value), validator.ErrorMessage(field));
        }
    }
}
