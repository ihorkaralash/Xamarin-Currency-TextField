# Currency TextField for Xamarin.Android and Xamarin.iOS

Format text field inputs as currency in your Xamarin.Android and Xamarin.iOS applications.

![alt text](https://raw.githubusercontent.com/ihorkaralash/Xamarin-Currency-TextField/master/art/android.png)![alt text](https://raw.githubusercontent.com/ihorkaralash/Xamarin-Currency-TextField/master/art/ios.png)

## NuGet

* Available on NuGet: [![Nuget](https://img.shields.io/nuget/v/CurrencyTextField?label=NuGet)](https://www.nuget.org/packages/CurrencyTextField/)

## Controls

* CurrencyEditText
* CurrencyTextField

## Using Android

```xml
<Plugin.TextField.Droid.CurrencyEditText
        android:layout_width="wrap_content"
        android:layout_height="wrap_content"
        android:hint="$0.00" />
```

## Using iOS

Inherit from CurrencyTextField

```csharp
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
```

![alt text](https://raw.githubusercontent.com/ihorkaralash/Xamarin-Currency-TextField/master/art/textfield.png)
