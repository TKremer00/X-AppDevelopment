using CommunityToolkit.Mvvm.Input;
using Friendsbook.Core.MVVM;
using Friendsbook.Pages;
using Friendsbook.UIHelpers;
using Friendsbook.Views;

namespace Friendsbook.ViewModels
{
    public class MainPageViewModel : ObservableObject
    {
        public MainPageViewModel(FriendsViewModel friendsViewModel)
        {
            FriendsViewModel = friendsViewModel;
            AddCommand = new RelayCommand(HandleAddCommand);
        }

        public RelayCommand AddCommand { get; }

        public FriendsViewModel FriendsViewModel { get; }

        public async Task UpdateFriends()
        {
            FriendsViewModel.UpdateFriends();
        }

        private async void HandleAddCommand()
        {
            await NavigationHelper.NavigateTo<FriendFormPage>();
        }

    }
}
