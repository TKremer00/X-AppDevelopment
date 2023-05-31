using Friendsbook.Core.Helpers;

namespace Friendsbook.Core.Controllers
{
    public class ImageController
    {

        private readonly IMediaPicker _mediaPicker;
        private bool _isCapturing;

        public ImageController(IMediaPicker mediaPicker)
        {
            _mediaPicker = mediaPicker;
        }

        public async Task<string> TakeImageAsync(Action<bool> isLoadingCallback)
        {
            if (_isCapturing)
                return null;

            _isCapturing = true;

            if (!_mediaPicker.IsCaptureSupported)
                throw new FeatureNotSupportedException("Device has no capture support");

            try
            {
                isLoadingCallback.Invoke(true);

                FileResult file = await _mediaPicker.CapturePhotoAsync();

                if (file == null)
                {
                    return null;
                }

                string localFilePath = PathHelper.GetGalleryFile(file.FileName);
                using FileStream localFileStream = File.OpenWrite(localFilePath);
#if WINDOWS
				// on Windows file.OpenReadAsync() throws an exception
				using Stream sourceStream = File.OpenRead(file.FullPath);
#else
                using Stream sourceStream = await file.OpenReadAsync();
#endif

                await sourceStream.CopyToAsync(localFileStream);

                return localFilePath;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                _isCapturing = false;
                isLoadingCallback.Invoke(false);
            }
        }
    }
}
