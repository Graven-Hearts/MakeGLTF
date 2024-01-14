using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;

namespace Graven.Hearts.MakeGLTF.Extensions
{
    public static class DialogExtensions
    {
        public static async Task<IStorageFile?> ShowOpenFileDialog(this Avalonia.Visual? visual)
        {
            // Get top level from the current control. Alternatively, you can use Window reference instead.
            var topLevel = TopLevel.GetTopLevel(visual);
            if (topLevel is null)
                return null;

            // Start async operation to open the dialog.
            var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Open Text File",
                AllowMultiple = false
            });

            if (files is null || !files.Any())
                return null;

            return files[0];
        }
    }
}
