using Friendsbook.Persistence.Models;

namespace Friendsbook.Core.Validators
{
    internal class ValidateFriend : IModelValidator<Friend>
    {
        public string[] Validate(Friend friend)
        {
            var errors = new List<string>();

            ValidateRequiredField(errors, friend.Firstname, nameof(friend.Firstname),
                () => ValidatorsHelper.Validate(ValidatorsHelper.NameValidators, friend.Firstname, nameof(friend.Firstname)));

            ValidateRequiredField(errors, friend.Lastname, nameof(friend.Lastname),
                    () => ValidatorsHelper.Validate(ValidatorsHelper.NameValidators, friend.Lastname, nameof(friend.Lastname)));

            ValidateRequiredField(errors, friend.HouseNumber, nameof(friend.HouseNumber), ValidatorsHelper.HouseNumberValidator);

            ValidateRequiredField(errors, friend.Street, nameof(friend.Street),
                    () => ValidatorsHelper.Validate(ValidatorsHelper.StringNoDigitLengthValidator(5), friend.Street, nameof(friend.Street)));

            ValidateRequiredField(errors, friend.City, nameof(friend.City),
                () => ValidatorsHelper.Validate(ValidatorsHelper.StringNoDigitLengthValidator(2), friend.City, nameof(friend.City)));

            ValidateRequiredField(errors, friend.Country, nameof(friend.Country),
                () => ValidatorsHelper.Validate(ValidatorsHelper.StringNoDigitLengthValidator(3), friend.Country, nameof(friend.Country)));

            ValidateRequiredField(errors, friend.PhoneNumber, nameof(friend.PhoneNumber), ValidatorsHelper.PhoneNumberValidator);

            ValidateRequiredField(errors, friend.Email, nameof(friend.Email), ValidatorsHelper.EmailValidator);

            return errors.ToArray();
        }

        private static void ValidateRequiredField<T>(List<string> errors, T field, string fieldName, Func<string[]> validator) where T : class
        {
            var (IsValid, ErrorMessage) = ValidatorsHelper.ValidateRequriedField(field, fieldName);

            if (IsValid)
            {
                errors.AddRange(validator.Invoke());
            }
            else
            {
                errors.Add(ErrorMessage);
            }
        }

        private static void ValidateRequiredField<T>(List<string> errors, T field, string fieldName, IValidator<T> validator) where T : class
        {
            var (IsValid, ErrorMessage) = ValidatorsHelper.ValidateRequriedField(field, fieldName);

            if (IsValid)
            {
                if (!validator.IsValid(field))
                {
                    errors.Add(validator.ErrorMessage());
                }
            }
            else
            {
                errors.Add(ErrorMessage);
            }
        }
    }
}
