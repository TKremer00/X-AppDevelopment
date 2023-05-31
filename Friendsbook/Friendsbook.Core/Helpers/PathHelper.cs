namespace Friendsbook.Core.Helpers
{
    public static class PathHelper
    {
        public static readonly string GalleryFolder = FileSystem.AppDataDirectory;

        public static string GetGalleryFile(string filename)
        {
            return Path.Combine(GalleryFolder, filename);
        }
    }
}
