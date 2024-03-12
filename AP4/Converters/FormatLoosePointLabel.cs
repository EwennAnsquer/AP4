using System.Diagnostics;

namespace AP4.Converters;

public class FormatLoosePointLabel : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        DateTime date = (DateTime)value;
        TimeSpan data = date - DateTime.Now;
        return data.Days.ToString();
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
