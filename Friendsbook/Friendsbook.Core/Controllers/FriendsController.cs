using Friendsbook.Core.Responses;
using Friendsbook.Core.Validators;
using Friendsbook.Persistence;
using Friendsbook.Persistence.Models;

namespace Friendsbook.Core.Controllers
{
    public class FriendsController
    {
        private FriendsbookContext _context;
        private IModelValidator<Friend> _validator;

        public FriendsController(FriendsbookContext context)
        {
            _context = context;
            _validator = new ValidateFriend();
        }

        public IResponse SaveFriend(Friend friend)
        {
            var validationMessages = ValidateFriend(friend);
            if (validationMessages.Any())
            {
                return new ErrorResponse(validationMessages);
            }

            return new SuccessResponse();
        }

        private string[] ValidateFriend(Friend friend)
        {
            return _validator.Validate(friend);
        }
    }
}
