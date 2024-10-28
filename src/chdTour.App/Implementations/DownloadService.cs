using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chdTour.App.Implementations
{
    public class DownloadService : IDownloadService
    {
        private readonly IJSRuntime _jSRuntime;

        public DownloadService(IJSRuntime jSRuntime)
        {
            this._jSRuntime = jSRuntime;
        }

        public async Task DownloadFileFromStream(string name, byte[] data)
        {
            var fileStream = GetFileStream(data);
            var fileName = name;

            using var streamRef = new DotNetStreamReference(stream: fileStream);
            await this._jSRuntime.InvokeVoidAsync("downloadFileFromStream", fileName, streamRef);
        }

        private Stream GetFileStream(byte[] data)
        {
            var fileStream = new MemoryStream(data);
            return fileStream;
        }
    }
    public interface IDownloadService
    {
        Task DownloadFileFromStream(string name, byte[] data);
    }
}
