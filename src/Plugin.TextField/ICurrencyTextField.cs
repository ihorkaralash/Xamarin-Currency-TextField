namespace Plugin.TextField
{
    public interface ICurrencyTextField
    {
        int MaxLength { get; set; }

        double DoubleValue { get; set; }

        int IntegerValue { get; set; }

        string CurrencySymbol { set; }

        string DecimalSeparator { set; }

        string GroupingSeparator { set; }
    }
}
