using System;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(AudioPhile.Droid.Controls.MyEntryRenderer))]
namespace AudioPhile.Droid.Controls
{
    public class MyEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null) return;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                Control.BackgroundTintList = ColorStateList.ValueOf(color: Android.Graphics.Color.White);

            else
                Control.Background.SetColorFilter(Android.Graphics.Color.White, PorterDuff.Mode.Src);
        }
    }
}
