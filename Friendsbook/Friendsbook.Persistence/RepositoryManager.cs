using Friendsbook.Persistence.Repositories;

namespace Friendsbook.Persistence
{
    public class RepositoryManager
    {
        private readonly FriendsbookContext _context;

        public FriendRepository Friends { get; set; }

        public RepositoryManager(FriendsbookContext context, FriendRepository friendRepository)
        {
            _context = context;
            Friends = friendRepository;
        }

        public Task<int> SaveAsync() => _context.SaveChangesAsync();

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
