using CommunityToolkit.Mvvm.Input;
using Friendsbook.Core.Controllers;
using Friendsbook.Core.MVVM;
using Friendsbook.Core.ValidationModels;
using Friendsbook.Pages;
using Friendsbook.Persistence.Models;
using Friendsbook.UIHelpers;

namespace Friendsbook.ViewModels
{
    public class FriendFormPageViewModel : ObservableObject
    {
        private readonly FriendsController _friendsController;
        private readonly ImageController _imageController;
        private string[] _validationMessages = Array.Empty<string>();
        private bool _isLoading;

        public FriendFormPageViewModel(FriendsController friendsController, ImageController imageController)
        {
            Friend = new FriendValidationModel(new Friend());
            _friendsController = friendsController;
            _imageController = imageController;
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

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                RaisePropertyChanged();
            }
        }

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

            await NavigateToMainView();
        }

        private async void HandleCancelButtonCommand()
        {
            await NavigateToMainView();
        }

        private async Task NavigateToMainView()
        {
            await NavigationHelper.NavigateTo<MainPage>();
        }

        private async void HandleTakePhotoCommand()
        {
            try
            {
                var imageLocation = await _imageController.TakeImageAsync(HandleLoading);
                if (string.IsNullOrEmpty(imageLocation))
                {
                    return;
                }

                Friend.Image = imageLocation;
                RaisePropertyChanged(nameof(HasProfilePicture));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private void HandleLoading(bool isLoading)
        {
            if (HasProfilePicture && isLoading)
                return;

            IsLoading = isLoading;
        }
    }
}
