using Friendsbook.Persistence.Models;

namespace Friendsbook.Persistence.Repositories
{
    public class FriendRepository : Repository<Friend>
    {
        public FriendRepository(FriendsbookContext context) : base(context)
        {
        }
    }
}
