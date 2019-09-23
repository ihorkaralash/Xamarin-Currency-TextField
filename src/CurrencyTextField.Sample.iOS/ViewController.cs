using System;
using UIKit;

namespace CurrencyTextField.Sample.iOS
{
    public partial class ViewController : UIViewController
    {
        public ViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            TextField.MaxLength = 10;
            TextField.Format = (string text, out string newText) =>
            {
                newText = "";

                return text == "$0.0";
            };
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            GetValueButton.TouchUpInside += GetValueButton_TouchUpInside;
        }

        public override void ViewDidDisappear(bool animated)
        {
            GetValueButton.TouchUpInside -= GetValueButton_TouchUpInside;

            base.ViewDidDisappear(animated);
        }

        private void GetValueButton_TouchUpInside(object sender, EventArgs e)
        {
            ValueLabel.Text = $"Clean double: {TextField.DoubleValue}\nClean integer: {TextField.IntegerValue}";
        }
    }
}