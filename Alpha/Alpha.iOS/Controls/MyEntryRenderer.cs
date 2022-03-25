using System;
using CoreAnimation;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Entry), typeof(Alpha.iOS.Controls.MyEntryRenderer))]
namespace Alpha.iOS.Controls
{
    public class MyEntryRenderer : EntryRenderer
    {
        private CALayer _line;

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            _line = null;

            if (Control == null || e.NewElement == null)
                return;

            Control.BorderStyle = UITextBorderStyle.None;

            _line = new CALayer
            {
                BorderColor = UIColor.Blue.CGColor,
                BackgroundColor = UIColor.White.CGColor,
                Frame = new CGRect(0, Frame.Height / 2, Frame.Width * 2, 1f)
            };

            Control.Layer.AddSublayer(_line);
        }
    }
}
