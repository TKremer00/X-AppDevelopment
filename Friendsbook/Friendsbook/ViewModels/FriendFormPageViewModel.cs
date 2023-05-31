using CommunityToolkit.Mvvm.Input;
using Friendsbook.Core.Controllers;
using Friendsbook.Core.Helpers;
using Friendsbook.Core.MVVM;
using Friendsbook.Core.ValidationModels;
using Friendsbook.Pages;
using Friendsbook.Persistence.Models;

namespace Friendsbook.ViewModels
{
    public class FriendFormPageViewModel : ObservableObject
    {
        private readonly FriendsController _friendsController;
        private string[] _validationMessages = Array.Empty<string>();

        public FriendFormPageViewModel(FriendsController friendsController)
        {
            Friend = new FriendValidationModel(new Friend());
            _friendsController = friendsController;
            SubmitButtonCommand = new RelayCommand(HandleSubmitButtonCommand);
            CancelButtonCommand = new RelayCommand(HandleCancelButtonCommand);
            TakePhotoCommand = new RelayCommand(HandleTakePhotoCommand);
        }

        public FriendValidationModel Friend { get; }

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

        public bool HasProfilePicture => !string.IsNullOrEmpty(Friend.Image);

        public RelayCommand SubmitButtonCommand { get; }

        public RelayCommand CancelButtonCommand { get; }

        public RelayCommand TakePhotoCommand { get; }

        private async void HandleSubmitButtonCommand()
        {
            if (!Friend.IsValid)
            {
                return;
            }

            await _friendsController.SaveFriend(Friend);

            NavigateToMainView();
        }

        private void HandleCancelButtonCommand()
        {
            NavigateToMainView();
        }

        private void NavigateToMainView()
        {
            NavigationHelper.Navigate<MainPage>();
        }

        private async void HandleTakePhotoCommand()
        {

        }
    }
}
