using System;
using CoreGraphics;
using Foundation;

namespace CurrencyTextField.Sample.iOS
{
    [Register("CustomTextField")]
    public class CustomTextField : Plugin.TextField.iOS.CurrencyTextField
    {
        public CustomTextField(CGRect frame) : base(frame)
        {
        }

        public CustomTextField(NSCoder coder) : base(coder)
        {
        }

        public CustomTextField(IntPtr handle) : base(handle)
        {
        }
    }
}
