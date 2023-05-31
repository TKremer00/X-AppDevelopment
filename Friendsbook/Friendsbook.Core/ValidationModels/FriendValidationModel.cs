using Friendsbook.Core.MVVM;
using Friendsbook.Persistence.Models;
using System.ComponentModel.DataAnnotations;

namespace Friendsbook.Core.ValidationModels
{
    public class FriendValidationModel : ObservableValidator
    {
        private string _image;
        private string _firstname;
        private string _lastname;
        private string _houseNumber;
        private string _street;
        private string _city;
        private string _country;
        private string _phoneNumber;
        private string _email;

        public FriendValidationModel()
        {
        }

        public FriendValidationModel(Friend friend)
        {
            Id = friend.Id;
            Firstname = friend.Firstname;
            Lastname = friend.Lastname;
            HouseNumber = friend.HouseNumber;
            Street = friend.Street;
            City = friend.City;
            Country = friend.Country;
            PhoneNumber = friend.PhoneNumber;
            Email = friend.Email;
        }

        public int Id { get; set; }

        public string Image
        {
            get => _image;
            set
            {
                SetProperty(ref _image, value, true);
                OnPropertyChanged(nameof(ImageErrors));
                OnPropertyChanged(nameof(IsImageValid));
            }
        }
        public IEnumerable<ValidationResult> ImageErrors => GetErrors(nameof(Image));

        public bool IsImageValid => !ImageErrors.Any();

        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Firstname
        {
            get => _firstname;
            set
            {
                SetProperty(ref _firstname, value, true);
                OnPropertyChanged(nameof(FirstnameErrors));
                OnPropertyChanged(nameof(IsFirstnameValid));
            }
        }

        public IEnumerable<ValidationResult> FirstnameErrors => GetErrors(nameof(Firstname));

        public bool IsFirstnameValid => !FirstnameErrors.Any();

        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Lastname
        {
            get => _lastname;
            set
            {
                SetProperty(ref _lastname, value, true);
                OnPropertyChanged(nameof(LastnameErrors));
                OnPropertyChanged(nameof(IsLastnameValid));
            }
        }

        public IEnumerable<ValidationResult> LastnameErrors => GetErrors(nameof(Lastname));

        public bool IsLastnameValid => !LastnameErrors.Any();

        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string HouseNumber
        {
            get => _houseNumber;
            set
            {
                SetProperty(ref _houseNumber, value, true);
                OnPropertyChanged(nameof(HouseNumberErrors));
                OnPropertyChanged(nameof(IsHouseNumberValid));
            }
        }

        public IEnumerable<ValidationResult> HouseNumberErrors => GetErrors(nameof(HouseNumber));

        public bool IsHouseNumberValid => !HouseNumberErrors.Any();


        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Street
        {
            get => _street;
            set
            {
                SetProperty(ref _street, value, true);
                OnPropertyChanged(nameof(StreetErrors));
                OnPropertyChanged(nameof(IsStreetValid));
            }
        }

        public IEnumerable<ValidationResult> StreetErrors => GetErrors(nameof(Street));

        public bool IsStreetValid => !StreetErrors.Any();

        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string City
        {
            get => _city;
            set
            {
                SetProperty(ref _city, value, true);
                OnPropertyChanged(nameof(CityErrors));
                OnPropertyChanged(nameof(IsCityValid));
            }
        }

        public IEnumerable<ValidationResult> CityErrors => GetErrors(nameof(City));

        public bool IsCityValid => !CityErrors.Any();

        [Required]
        [MinLength(2)]
        [MaxLength(200)]
        public string Country
        {
            get => _country;
            set
            {
                SetProperty(ref _country, value, true);
                OnPropertyChanged(nameof(CountryErrors));
                OnPropertyChanged(nameof(IsCountryValid));
            }
        }

        public IEnumerable<ValidationResult> CountryErrors => GetErrors(nameof(Country));

        public bool IsCountryValid => !CountryErrors.Any();

        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                SetProperty(ref _phoneNumber, value, true);
                OnPropertyChanged(nameof(PhoneNumberErrors));
                OnPropertyChanged(nameof(IsPhoneNumberValid));
            }
        }

        public IEnumerable<ValidationResult> PhoneNumberErrors => GetErrors(nameof(PhoneNumber));

        public bool IsPhoneNumberValid => !PhoneNumberErrors.Any();

        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Email
        {
            get => _email;
            set
            {
                SetProperty(ref _email, value, true);
                OnPropertyChanged(nameof(EmailErrors));
                OnPropertyChanged(nameof(IsEmailValid));
            }
        }

        public IEnumerable<ValidationResult> EmailErrors => GetErrors(nameof(Email));

        public bool IsEmailValid => !EmailErrors.Any();

        internal Friend ConvertToFriend()
        {
            return new Friend
            {
                Id = Id,
                Image = Image,
                Firstname = Firstname,
                Lastname = Lastname,
                HouseNumber = HouseNumber,
                Street = Street,
                City = City,
                Country = Country,
                PhoneNumber = PhoneNumber,
                Email = Email,
            };
        }
    }
}
