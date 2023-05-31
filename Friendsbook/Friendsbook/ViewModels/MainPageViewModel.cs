using CommunityToolkit.Mvvm.Input;
using Friendsbook.Core.Controllers;
using Friendsbook.Core.Helpers;
using Friendsbook.Core.MVVM;
using Friendsbook.Pages;

namespace Friendsbook.ViewModels
{
    public class MainPageViewModel : ObservableObject
    {
        public MainPageViewModel(FriendsController friendsController)
        {
            AddCommand = new RelayCommand(HandleAddCommand);
        }

        public RelayCommand AddCommand { get; }

        private void HandleAddCommand()
        {
            NavigationHelper.Navigate<FriendFormPage>();
        }

    }
}
