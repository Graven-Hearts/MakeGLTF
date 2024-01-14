using System.IO;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;

namespace Graven.Hearts.MakeGLTF.Extensions
{
    public static class ImageExtensions
    {
        public static async Task<(IStorageFile File, WriteableBitmap? Bitmap)> GetImage(this Avalonia.Visual? visual)
        {
            var storageFile = await visual.ShowOpenFileDialog();
            if (storageFile is null)
                return default;

            return (storageFile, await storageFile.ReadImage());
        }

        public static async Task<WriteableBitmap?> ReadImage(this IStorageFile storageFile)
        {
            if (storageFile is null)
                return null;

            // Open reading stream from the first file.
            await using var stream = await storageFile.OpenReadAsync();
            return WriteableBitmap.Decode(stream);
        }

        public static WriteableBitmap? ReadImage(this string filePath)
        {
            if (filePath.IsNullOrEmpty())
                return null;

            // Open reading stream from the first file.
            using var stream = File.Open(filePath, FileMode.Open);
            return WriteableBitmap.Decode(stream);
        }
    }
}
