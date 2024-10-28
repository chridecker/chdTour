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

        public async Task<byte[]> PickFile(CancellationToken cancellationToken)
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
            if (file is null) { return []; }
            this.ContentType = file.ContentType;
            byte[] buffer = new byte[16 * 1024];
            using var ms = new MemoryStream();
            using var fileStream = await file.OpenReadAsync();
            int read;
            while ((read = await fileStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, read);
            }
            return ms.ToArray();
        }
    }
    public interface ICustomFilePicker
    {
        string ContentType { get; }

        Task<byte[]> PickFile(CancellationToken cancellationToken);
    }
}
