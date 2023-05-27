using CommunityToolkit.Mvvm.Input;
using Friendsbook.Core.Controllers;
using Friendsbook.Core.MVVM;
using Friendsbook.Persistence.Models;

namespace Friendsbook.ViewModels
{
    internal class FriendFormPageViewModel : ObservableObject
    {
        private readonly FriendsController _friendsController;
        private string[] _validationMessages = Array.Empty<string>();

        public FriendFormPageViewModel(FriendsController friendsController) : this(new Friend(), friendsController)
        {
        }

        public FriendFormPageViewModel(Friend friend, FriendsController friendsController)
        {
            Friend = friend;
            _friendsController = friendsController;
            SubmitButtonCommand = new RelayCommand(HandleSubmitButtonCommand);
        }

        public Friend Friend { get; }

        public string SubmitButtonText => Friend.Id == 0 ? "Create" : "Update";

        public string[] ValidationMessages
        {
            get => _validationMessages;
            set
            {
                _validationMessages = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(HasValidationMessage));
            }
        }

        public bool HasValidationMessage => ValidationMessages.Any();

        public RelayCommand SubmitButtonCommand { get; }


        private void HandleSubmitButtonCommand()
        {
            var response = _friendsController.SaveFriend(Friend);

            if (response.IsSuccess)
            {
                return;
            }

            ValidationMessages = response.ErrorMessages;
        }
    }
}
