#if ANDROID
using Android.Webkit;
using AndroidX.Activity;
using Bumptech.Glide.Load.Engine;
using chdTour.App.Platforms.Android;
#endif
using Microsoft.AspNetCore.Components.WebView;
using Microsoft.Maui.Platform;
using System.Text.RegularExpressions;

namespace chdTour.App
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            try
            {
                this.blazorWebView.BlazorWebViewInitializing += this.BlazorWebViewInitializing;
                this.blazorWebView.BlazorWebViewInitialized += this.BlazorWebViewInitialized;
            }
            catch (Exception ex)
            {
                // do something if error appears
            }
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
#if ANDROID
            await this.CheckPermissions();
#endif
        }

        private partial void BlazorWebViewInitializing(object? sender, BlazorWebViewInitializingEventArgs e);
        private partial void BlazorWebViewInitialized(object? sender, BlazorWebViewInitializedEventArgs e);

#if WINDOWS
         private partial void BlazorWebViewInitializing(object? sender, BlazorWebViewInitializingEventArgs e)
        {
        }

        private partial void BlazorWebViewInitialized(object? sender, BlazorWebViewInitializedEventArgs e)
        {
        }
#endif


#if ANDROID

        // To manage Android permissions, update AndroidManifest.xml to include the permissions and
        // features required by your app. You may have to perform additional configuration to enable
        // use of those APIs from the WebView, as is done below. A custom WebChromeClient is needed
        // to define what happens when the WebView requests a set of permissions. See
        // PermissionManagingBlazorWebChromeClient.cs to explore the approach taken in this example.

        private partial void BlazorWebViewInitializing(object? sender, BlazorWebViewInitializingEventArgs e)
        {
        }
        private async Task CheckPermissions()
        {
            PermissionStatus statusNotification = await Permissions.RequestAsync<PermissionValidator>();
        }


        private partial void BlazorWebViewInitialized(object? sender, BlazorWebViewInitializedEventArgs e)
        {
            try
            {
                e.WebView.Download += this.WebView_DownloadAsync;

                if (e.WebView.Context?.GetActivity() is not ComponentActivity activity)
                {
                    throw new InvalidOperationException($"The permission-managing WebChromeClient requires that the current activity be a '{nameof(ComponentActivity)}'.");
                }

                e.WebView.Settings.JavaScriptEnabled = true;
                e.WebView.Settings.AllowFileAccess = true;
                e.WebView.Settings.MediaPlaybackRequiresUserGesture = false;
                var webChromeClient = new PermissionManagingBlazorWebChromeClient(e.WebView.WebChromeClient!, activity);
                e.WebView.SetWebChromeClient(webChromeClient);
            }
            catch (Exception)
            {
                // do something if error appears
            }

        }

        private async void WebView_DownloadAsync(object sender, DownloadEventArgs e)
        {
            Uri uri = new Uri(e.Url);
            await DownloadAsync(uri, e.Mimetype);
        }


        public static string DataUrl2Filename(string base64encodedstring)
        {
            var filename = Regex.Match(base64encodedstring, @"data:text/(?<filename>.+?);(?<type2>.+?),(?<data>.+)").Groups["filename"].Value;
            return filename;
        }
        public static byte[] DataUrl2Bytes(string base64encodedstring)
        {
            var base64Data = Regex.Match(base64encodedstring, @"data:text/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
            var bytes = Convert.FromBase64String(base64Data);
            return bytes;
        }

#endif

        private async Task DownloadAsync(Uri uri, string? mimeType = null)
        {
            var UploadPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uploads");

            string fileName = Path.GetFileName(uri.LocalPath);
            var httpClient = new HttpClient();
            var filePath = Path.Combine(UploadPath, fileName);
#if ANDROID
            if (uri.Scheme == "data")
            {
                fileName = DataUrl2Filename(uri.OriginalString);
                filePath = Path.Combine(UploadPath, $"{DateTime.Now.ToString("yyyy-MM-dd-hhmmss")}-{fileName}");
                var bytes = DataUrl2Bytes(uri.OriginalString);
                File.WriteAllBytes(filePath, bytes);
                await DisplayAlert("Download", $"Fertig {fileName}", "OK");
                return;
            }
#endif
            byte[] fileBytes = await httpClient.GetByteArrayAsync(uri);
            File.WriteAllBytes(filePath, fileBytes);
            await DisplayAlert("Download", $"Fertig {fileName}", "OK");
        }
    }
}
