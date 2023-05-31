using Friendsbook.Core.Responses;
using Friendsbook.Core.ValidationModels;
using Friendsbook.Persistence;
using Friendsbook.Persistence.Models;

namespace Friendsbook.Core.Controllers
{
    public class FriendsController
    {
        private RepositoryManager _repositoryManager;

        public FriendsController(RepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<IResponse> SaveFriend(FriendValidationModel validatedFriend)
        {
#if DEBUG
            if (!validatedFriend.IsValid)
            {
                throw new ArgumentException("The friend must be valid");
            }
#endif
            var friend = validatedFriend.ConvertToFriend();

            await _repositoryManager.Friends.AddAsync(friend);
            await _repositoryManager.SaveAsync();

            return new SuccessResponse();
        }

        public async Task<IEnumerable<Friend>> GetFriends()
        {
            return await _repositoryManager.Friends.GetAllAsync();
        }

        public async Task<string> TakePhoto()
        {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

            if (photo == null)
            {
                return null;
            }

            // save the file into local storage
            string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

            using Stream sourceStream = await photo.OpenReadAsync();
            using FileStream localFileStream = File.OpenWrite(localFilePath);

            await sourceStream.CopyToAsync(localFileStream);

            return localFilePath;
        }
    }
}
