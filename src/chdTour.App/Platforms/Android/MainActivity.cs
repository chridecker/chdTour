using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Webkit;
using AndroidX.Core.Content;

namespace chdTour.App
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {

        public static IValueCallback mUploadCallbackAboveL;
        public static Android.Net.Uri imageUri;
        public static MainActivity Instance;
        public static IValueCallback mUploadMessage;
        public static int FILECHOOSER_RESULTCODE = 1;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Instance = this;

            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.Camera) != (int)Permission.Granted)
            {

            }
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            if (requestCode == FILECHOOSER_RESULTCODE)
            {
                if (null == mUploadMessage) return;
                Android.Net.Uri result = intent == null || resultCode != Result.Ok ? null : intent.Data;
                mUploadMessage.OnReceiveValue(result);
                mUploadMessage = null;
            }
        }
        private void onActivityResultAboveL(int requestCode, Result resultCode, Intent data)
        {
            if (mUploadCallbackAboveL == null)
            {
                return;
            }
            Android.Net.Uri[] results = null;
            if (resultCode == Result.Ok)
            {

                results = new Android.Net.Uri[] { imageUri };
                results[0] = MainActivity.imageUri;

                //  The data.Data property contains the file Uri of your choice, which you can manipulate through this Uri
            }
            mUploadCallbackAboveL.OnReceiveValue(results);
            mUploadCallbackAboveL = null;
        }
    }
}
