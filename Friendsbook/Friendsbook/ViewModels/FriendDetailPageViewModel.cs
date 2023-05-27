using Friendsbook.Core.MVVM;
using Friendsbook.Persistence.Models;

namespace Friendsbook.ViewModels
{
    internal class FriendDetailPageViewModel : ObservableObject
    {
        public FriendDetailPageViewModel(int id)
        {
            // TODO: get friend from id
            Friend = new Friend();
        }

        public Friend Friend { get; }

        public bool HasProfilePicture => Friend.Image != null;

    }
}
