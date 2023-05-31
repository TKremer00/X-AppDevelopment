using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.System;
using WinRT.Interop;

namespace Friendsbook
{
    public partial class ImagePicker
    {
        public partial Task<FileResult> CapturePhotoAsync(MediaPickerOptions options)
            => CaptureAsync();

        public partial Task<FileResult> CaptureVideoAsync(MediaPickerOptions options)
            => throw new FeatureNotSupportedException("This app doesn't support taking videos");

        private async Task<FileResult> CaptureAsync()
        {
            var captureUi = new CustomCameraCaptureUI();

            StorageFile file = await captureUi.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (file != null)
            {
                return new FileResult(file.Path, file.ContentType);
            }

            return null;
        }

        private class CustomCameraCaptureUI
        {
            private readonly LauncherOptions _launcherOptions;

            public CustomCameraCaptureUI()
            {
                var window = WindowStateManager.Default.GetActiveWindow();
                var handle = WindowNative.GetWindowHandle(window);

                _launcherOptions = new LauncherOptions();
                InitializeWithWindow.Initialize(_launcherOptions, handle);

                _launcherOptions.TreatAsUntrusted = false;
                _launcherOptions.DisplayApplicationPicker = false;
                _launcherOptions.TargetApplicationPackageFamilyName = "Microsoft.WindowsCamera_8wekyb3d8bbwe";
            }

            public async Task<StorageFile> CaptureFileAsync(CameraCaptureUIMode mode)
            {
                const string EXTENSION = ".jpg";

                var currentAppData = ApplicationData.Current;
                var tempLocation = currentAppData.LocalCacheFolder;
                var tempFileName = $"capture{EXTENSION}";
                var tempFile = await tempLocation.CreateFileAsync(tempFileName, CreationCollisionOption.GenerateUniqueName);
                var token = Windows.ApplicationModel.DataTransfer.SharedStorageAccessManager.AddFile(tempFile);

                var set = new ValueSet
                {
                    { "MediaType", "photo" },
                    { "PhotoFileToken", token }
                };

                var uri = new Uri("microsoft.windows.camera.picker:");
                var result = await Windows.System.Launcher.LaunchUriForResultsAsync(uri, _launcherOptions, set);
                if (result.Status == LaunchUriStatus.Success && result.Result != null)
                {
                    return tempFile;
                }

                return null;
            }
        }


    }
}
