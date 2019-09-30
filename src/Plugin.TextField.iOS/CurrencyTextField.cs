using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Plugin.TextField.iOS
{
    [Register("CurrencyTextField")]
    public class CurrencyTextField : UITextField, ICurrencyTextField
    {
        private string _previousValue = "";

        protected readonly NSNumberFormatter CurrencyFormatter = new NSNumberFormatter();

        public CurrencyTextField(CGRect frame) : base(frame)
        {
            InitTextField();
        }

        public CurrencyTextField(NSCoder coder) : base(coder)
        {
            InitTextField();
        }

        public CurrencyTextField(IntPtr handle) : base(handle)
        {
            InitTextField();
        }

        public int MaxLength { get; set; }

        public double DoubleValue
        {
            get => Amount;
            set => Amount = value;
        }

        public int IntegerValue
        {
            get => (int) Amount;
            set => Amount = value;
        }

        public string CurrencySymbol
        {
            set => CurrencyFormatter.CurrencySymbol = value;
        }

        public string DecimalSeparator
        {
            set => CurrencyFormatter.CurrencyDecimalSeparator = value;
        }

        public string GroupingSeparator
        {
            set => CurrencyFormatter.CurrencyGroupingSeparator = value;
        }

        public override void WillMoveToSuperview(UIView newsuper)
        {
            if (newsuper != null)
            {
                NSNotificationCenter.DefaultCenter.AddObserver(TextFieldTextDidChangeNotification, TextDidChange);
            }
            else
            {
                NSNotificationCenter.DefaultCenter.RemoveObserver(this);
            }
        }

        private void InitTextField()
        {
            MaxLength = 12;

            KeyboardType = UIKeyboardType.DecimalPad;

            CurrencyFormatter.NumberStyle = NSNumberFormatterStyle.Currency;
            CurrencyFormatter.MinimumFractionDigits = 2;
            CurrencyFormatter.MaximumFractionDigits = 2;
        }

        private double Amount
        {
            get
            {
                var cleanNumericString = GetCleanNumberString();
                if (double.TryParse(cleanNumericString, out var textFieldNumber))
                {
                    return textFieldNumber / 100;
                }

                return 0;
            }
            set
            {
                var textFieldStringValue = Text = CurrencyFormatter.StringFromNumber(new NSNumber(value));
                if (!string.IsNullOrEmpty(textFieldStringValue))
                {
                    _previousValue = textFieldStringValue;
                }
            }
        }

        public FormatDelegate Format { get; set; }

        private int GetOriginalCursorPosition()
        {
            var cursorOffset = 0;
            var startPosition = BeginningOfDocument;
            var selectedTextRange = SelectedTextRange;

            if (startPosition != null && selectedTextRange != null)
            {
                cursorOffset = (int) GetOffsetFromPosition(startPosition, selectedTextRange.Start);
            }

            return cursorOffset;
        }

        private string GetCleanNumberString()
        {
            var cleanNumericString = "";
            var textFieldString = Text;

            if (!string.IsNullOrEmpty(textFieldString))
            {
                var toArray = textFieldString.Split(CurrencyFormatter.CurrencySymbol);
                cleanNumericString = string.Join("", toArray);

                toArray = cleanNumericString.Split(CurrencyFormatter.CurrencyGroupingSeparator);
                cleanNumericString = string.Join("", toArray);

                toArray = cleanNumericString.Split(CurrencyFormatter.CurrencyDecimalSeparator);
                cleanNumericString = string.Join("", toArray);
            }

            return cleanNumericString;
        }

        private void SetCursorOriginalPosition(int cursorOffset, int? oldTextFieldLength)
        {
            var newLength = Text?.Length;
            var startPosition = BeginningOfDocument;

            if (startPosition != null && oldTextFieldLength.HasValue && newLength.HasValue && oldTextFieldLength > cursorOffset)
            {
                var newOffset = newLength - oldTextFieldLength + cursorOffset;
                var newCursorPosition = GetPosition(startPosition, newOffset.Value);

                if (newCursorPosition != null)
                {
                    var newSelectedRange = GetTextRange(newCursorPosition, newCursorPosition);
                    SelectedTextRange = newSelectedRange;
                }
            }
        }

        private void TextDidChange(NSNotification notification)
        {
            var cursorOffset = GetOriginalCursorPosition();
            var cleanNumericString = GetCleanNumberString();
            var textFieldLength = Text?.Length;

            if (Format != null && Format(Text, out string newString))
            {
                _previousValue = Text = newString;
                return;
            }

            if (cleanNumericString.Length > MaxLength)
            {
                Text = _previousValue;
            }
            else
            {
                if (double.TryParse(cleanNumericString, out var textFieldNumber))
                {
                    Amount = textFieldNumber / 100;
                }
                else
                {
                    Text = _previousValue;
                }
            }

            SetCursorOriginalPosition(cursorOffset, textFieldLength);
        }
    }
}
