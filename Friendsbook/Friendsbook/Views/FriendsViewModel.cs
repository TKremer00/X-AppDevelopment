using Friendsbook.Core.MVVM;
using Friendsbook.Persistence.Models;

namespace Friendsbook.Views
{
    internal class FriendsViewModel : ObservableObject
    {
        public FriendsViewModel()
        {
            // TODO: get this from database
            Friends = new List<FriendListItemViewModel>()
            {
                new FriendListItemViewModel(new Friend { Id = 1, Firstname = "Wochi", Lastname = "Doe", })
            };
        }

        public IEnumerable<FriendListItemViewModel> Friends { get; private set; }
    }
}
