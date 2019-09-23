using System;
using Android.Content;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Util;
using Java.Lang;
using Java.Text;

namespace Plugin.TextField.Droid
{
    [Register("Plugin.TextField.Droid.CurrencyEditText")]
    public class CurrencyEditText : AppCompatEditText, ICurrencyTextField
    {
        private string _previousValue = "";

        private int _maxLength;
        public int MaxLength
        {
            get => _maxLength;
            set
            {
                _maxLength = value;

                SetFilters(new IInputFilter[]
                {
                    new InputFilterLengthFilter(value)
                });
            }
        }

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

        public string CurrencySymbol { get; set; }

        public string DecimalSeparator { get; set; }

        public string GroupingSeparator { get; set; }

        public FormatDelegate Format { get; set; }

        protected CurrencyEditText(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            InitTextField();
        }

        public CurrencyEditText(Context context) : base(context)
        {
            InitTextField();
        }

        public CurrencyEditText(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            InitTextField();
        }

        public CurrencyEditText(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            InitTextField();
        }

        private void InitTextField()
        {
            MaxLength = 12;

            InputType = InputTypes.ClassNumber | InputTypes.NumberFlagDecimal;

            CurrencySymbol = "$";
            GroupingSeparator = ",";
            DecimalSeparator = ".";

            AddTextChangedListener(new CurrencyEditTextChangeListener
            {
                TextChanged = delegate(ITextWatcher sender, ICharSequence sequence, int start, int before, int end)
                {
                    if (Format != null && Format(Text, out string newString))
                    {
                        _previousValue = newString;
                        Text = newString;
                        return;
                    }

                    var newStr = sequence.ToString();
                    if (!newStr.Equals(_previousValue))
                    {
                        var cleanNumericString = GetCleanNumberString();

                        RemoveTextChangedListener(sender);

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
                        SetSelection(Text.Length);

                        AddTextChangedListener(sender);
                    }
                }
            });
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
                var textFieldStringValue = Text = NumberFormat.CurrencyInstance.Format((value)).Replace(NumberFormat.CurrencyInstance.Currency.Symbol, CurrencySymbol);
                if (!string.IsNullOrEmpty(textFieldStringValue))
                {
                    _previousValue = textFieldStringValue;
                }
            }
        }

        private string GetCleanNumberString()
        {
            var cleanNumericString = "";
            var textFieldString = Text;

            if (!string.IsNullOrEmpty(textFieldString))
            {
                var toArray = textFieldString.Split(CurrencySymbol);
                cleanNumericString = string.Join("", toArray);

                toArray = cleanNumericString.Split(GroupingSeparator);
                cleanNumericString = string.Join("", toArray);

                toArray = cleanNumericString.Split(DecimalSeparator);
                cleanNumericString = string.Join("", toArray);
            }

            return cleanNumericString;
        }
    }
}