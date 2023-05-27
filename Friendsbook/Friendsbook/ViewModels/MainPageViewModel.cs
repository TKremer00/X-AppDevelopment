using CommunityToolkit.Mvvm.Input;
using Friendsbook.Core.Controllers;
using Friendsbook.Core.MVVM;
using Friendsbook.Pages;

namespace Friendsbook.ViewModels
{
    internal class MainPageViewModel : ObservableObject
    {
        private readonly FriendsController _friendController;

        public MainPageViewModel()
        {
            //TODO: fix context
            _friendController = new FriendsController(null);

            AddCommand = new RelayCommand(HandleAddCommand);
        }

        public RelayCommand AddCommand { get; }

        private void HandleAddCommand()
        {
            Application.Current.MainPage = new NavigationPage(new FriendFormPage(_friendController));
        }

    }
}
