using Foundation;
using Plugin.TextField;
using Plugin.TextField.iOS;
using UIKit;

namespace Pex.Mobile.iOS.UI.Views
{
    public class CurrencyTextFieldDelegate : NSObject, IUITextFieldDelegate, ICurrencyTextFieldDelegate
    {
        private readonly UITextField _textField;
        private string _previousValue = "";

        protected readonly NSNumberFormatter CurrencyFormatter = new NSNumberFormatter();

        public int MaxLength { get; set; }

        public double DoubleValue
        {
            get => Amount;
            set => Amount = value;
        }

        public int IntegerValue
        {
            get => (int)Amount;
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

        public FormatDelegate Format { get; set; }

        public CurrencyTextFieldDelegate(UITextField textField)
        {
            _textField = textField;

            MaxLength = 12;

            _textField.KeyboardType = UIKeyboardType.DecimalPad;

            CurrencyFormatter.NumberStyle = NSNumberFormatterStyle.Currency;
            CurrencyFormatter.MinimumFractionDigits = 2;
            CurrencyFormatter.MaximumFractionDigits = 2;
        }

        private double Amount
        {
            get
            {
                var cleanNumericString = GetCleanNumberString(_textField.Text);
                if (double.TryParse(cleanNumericString, out var textFieldNumber))
                {
                    return textFieldNumber / 100;
                }

                return 0;
            }
            set
            {
                var textFieldStringValue = _textField.Text = CurrencyFormatter.StringFromNumber(new NSNumber(value));
                if (!string.IsNullOrEmpty(textFieldStringValue))
                {
                    _previousValue = textFieldStringValue;
                }
            }
        }

        private int GetOriginalCursorPosition()
        {
            var cursorOffset = 0;
            var startPosition = _textField.BeginningOfDocument;
            var selectedTextRange = _textField.SelectedTextRange;

            if (selectedTextRange != null)
            {
                cursorOffset = (int)_textField.GetOffsetFromPosition(startPosition, selectedTextRange.Start);
            }

            return cursorOffset;
        }

        private string GetCleanNumberString(string textFieldString)
        {
            var cleanNumericString = "";

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
            var newLength = _textField.Text?.Length;
            var startPosition = _textField.BeginningOfDocument;

            if (oldTextFieldLength.HasValue && newLength.HasValue && oldTextFieldLength > cursorOffset)
            {
                var newOffset = newLength - oldTextFieldLength + cursorOffset;
                var newCursorPosition = _textField.GetPosition(startPosition, newOffset.Value);

                if (newCursorPosition != null)
                {
                    var newSelectedRange = _textField.GetTextRange(newCursorPosition, newCursorPosition);
                    _textField.SelectedTextRange = newSelectedRange;
                }
            }
        }

        [Export("textField:shouldChangeCharactersInRange:replacementString:")]
        public bool ShouldChangeCharacters(UITextField textField, NSRange range, string replacementString)
        {
            var text = string.IsNullOrEmpty(replacementString) ?
                 _textField.Text.Remove((int)range.Location, (int)range.Length) : _textField.Text.Insert((int)range.Location, replacementString);

            if (Format != null && Format(text, out string newString))
            {
                _previousValue = _textField.Text = newString;
                return false;
            }

            var cursorOffset = GetOriginalCursorPosition() + (string.IsNullOrEmpty(replacementString) ? -replacementString.Length : replacementString.Length);
            var cleanNumericString = GetCleanNumberString(text);
            var textFieldLength = text.Length;

            if (cleanNumericString.Length > MaxLength)
            {
                _textField.Text = _previousValue;
            }
            else
            {
                if (double.TryParse(cleanNumericString, out var textFieldNumber))
                {
                    Amount = textFieldNumber / 100;
                }
                else
                {
                    _textField.Text = _previousValue;
                }
            }

            SetCursorOriginalPosition(cursorOffset, textFieldLength);

            return false;
        }
    }
}