using Friendsbook.Core.Responses;
using Friendsbook.Core.ValidationModels;
using Friendsbook.Persistence;

namespace Friendsbook.Core.Controllers
{
    public class FriendsController
    {
        private FriendsbookContext _context;

        public FriendsController(FriendsbookContext context)
        {
            _context = context;
        }

        public IResponse SaveFriend(FriendValidationModel validatedFriend)
        {
#if DEBUG
            if (!validatedFriend.IsValid)
            {
                throw new ArgumentException("The friend must be valid");
            }
#endif
            // TODO: save the friend to the database
            var friend = validatedFriend.ConvertToFriend();

            return new SuccessResponse();
        }
    }
}
