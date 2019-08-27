# Currency TextField for Xamarin.iOS

Format text field inputs as currency in your Xamarin.iOS applications.

![alt text](https://raw.githubusercontent.com/ihorkaralash/Xamarin-Currency-TextField/master/art/ios1.png)

## NuGet
* Available on NuGet: [![NuGet](https://img.shields.io/nuget/v/IconFonts.svg?label=NuGet)](https://www.nuget.org/packages/IconFonts/)

## Controls

* CurrencyTextField

## Using

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
