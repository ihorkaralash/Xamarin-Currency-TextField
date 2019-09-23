using System;
using Android.Text;
using Java.Lang;
using Object = Java.Lang.Object;

namespace Plugin.TextField.Android
{
    public class CurrencyEditTextChangeListener : Object, ITextWatcher
    {
        public Action<ITextWatcher, ICharSequence, int, int, int> TextChanged;

        public void AfterTextChanged(IEditable s)
        {

        }

        public void BeforeTextChanged(ICharSequence s, int start, int count, int after)
        {

        }

        public void OnTextChanged(ICharSequence s, int start, int before, int count)
        {
            TextChanged?.Invoke(this, s, start, before, count);
        }
    }
}