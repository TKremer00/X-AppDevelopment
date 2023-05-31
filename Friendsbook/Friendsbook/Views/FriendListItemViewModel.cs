using CommunityToolkit.Mvvm.Input;
using Friendsbook.Core.MVVM;
using Friendsbook.Persistence.Models;

namespace Friendsbook.Views
{
    internal class FriendListItemViewModel : ObservableObject
    {
        public FriendListItemViewModel(Friend friend)
        {
            Friend = friend;
            ClickCommand = new RelayCommand(HandleClickCommand);
        }

        public RelayCommand ClickCommand { get; }

        public Friend Friend { get; }

        public bool HasProfilePicture => Friend.Image != null;

        private void HandleClickCommand()
        {
            // Application.Current.MainPage = new NavigationPage(new FriendDetailPage(Friend.Id));
        }
    }
}
