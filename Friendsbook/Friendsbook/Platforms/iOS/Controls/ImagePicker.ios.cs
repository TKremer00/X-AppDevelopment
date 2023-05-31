namespace Friendsbook
{
    public partial class ImagePicker
    {
        public partial Task<FileResult> CapturePhotoAsync(MediaPickerOptions options)
            => MediaPicker.Default.CapturePhotoAsync(options);

        public partial Task<FileResult> CaptureVideoAsync(MediaPickerOptions options)
            => throw new FeatureNotSupportedException("This app doesn't support taking videos");
    }
}
