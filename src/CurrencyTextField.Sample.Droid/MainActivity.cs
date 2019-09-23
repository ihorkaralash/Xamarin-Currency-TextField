using System;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Plugin.TextField.Droid;

namespace CurrencyTextField.Sample.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            FindViewById<CurrencyEditText>(Resource.Id.valueEditText).Format = (string text, out string newText) =>
            {
                newText = "";

                return text == "$0.0";
            };
        }

        protected override void OnResume()
        {
            base.OnResume();

            FindViewById(Resource.Id.getValueButton).Click += OnGetValueButtonClick;
        }

        protected override void OnPause()
        {
            FindViewById(Resource.Id.getValueButton).Click -= OnGetValueButtonClick;

            base.OnPause();
        }

        private void OnGetValueButtonClick(object sender, EventArgs e)
        {
            FindViewById<TextView>(Resource.Id.valueTextView).Text =
                $"Clean double: {FindViewById<CurrencyEditText>(Resource.Id.valueEditText).DoubleValue}" +
                $"\nClean integer: {FindViewById<CurrencyEditText>(Resource.Id.valueEditText).IntegerValue}";
        }
    }
}