using Friendsbook.Core.Controllers;
using Friendsbook.Core.MVVM;

namespace Friendsbook.Views
{
    public class FriendsViewModel : ObservableObject
    {
        private readonly FriendsController _friendsController;
        private IEnumerable<FriendListItemViewModel> _friends = Array.Empty<FriendListItemViewModel>();
        private string _search;

        public FriendsViewModel(FriendsController friendsController)
        {
            _friendsController = friendsController;
            _ = UpdateFriends();
        }

        public async Task UpdateFriends()
        {
            var friends = await _friendsController.GetFriends();
            _friends = friends.Select(x => new FriendListItemViewModel(x));
        }

        public string Search
        {
            get => _search;
            set
            {
                _search = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(Friends));
            }
        }

        public IEnumerable<FriendListItemViewModel> Friends => _friends.Where(FilterFriends);

        private bool FilterFriends(FriendListItemViewModel friendListItemView)
        {
            if (string.IsNullOrEmpty(Search))
            {
                return true;
            }

            return friendListItemView.Friend.FullName.Contains(Search, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
