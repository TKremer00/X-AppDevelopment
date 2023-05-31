using CommunityToolkit.Mvvm.Input;
using Friendsbook.Core.MVVM;
using Friendsbook.Persistence.Models;

namespace Friendsbook.Views
{
    public class FriendListItemViewModel : ObservableObject
    {
        public FriendListItemViewModel(Friend friend)
        {
            Friend = friend;
        }

        public RelayCommand ClickCommand { get; }

        public Friend Friend { get; }

        public bool HasProfilePicture => Friend.Image != null;
    }
}
