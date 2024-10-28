using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chdTour.App.Implementations
{
    public class CustomFilePicker : ICustomFilePicker
    {
        public string ContentType { get; set; }

        public async Task<Stream> PickFile(CancellationToken cancellationToken)
        {
            var file = await FilePicker.Default.PickAsync(new PickOptions
            {
                FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.Android, new[] { "image/jpeg","image/png","application/pdf" } }, // MIME type
                    { DevicePlatform.WinUI, new[] { ".pdf", ".jpg",".png" } }, // file extension
                }),
                PickerTitle = "Datei-Auswahl"
            });
            if (file is null) { return Stream.Null; }
            this.ContentType = file.ContentType;
            using var ms = new MemoryStream();
            using var fileStream = await file.OpenReadAsync();
            await fileStream.CopyToAsync(ms, cancellationToken);
            return ms;
        }
    }
    public interface ICustomFilePicker
    {
        string ContentType { get; }

        Task<Stream> PickFile(CancellationToken cancellationToken);
    }
}
