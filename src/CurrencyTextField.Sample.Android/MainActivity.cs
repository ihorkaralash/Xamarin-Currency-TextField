using System;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Plugin.TextField.Android;

namespace CurrencyTextField.Sample.Android
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

            global::Android.Support.V7.Widget.Toolbar toolbar = FindViewById<global::Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

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

