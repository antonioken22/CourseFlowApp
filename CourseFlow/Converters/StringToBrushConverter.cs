using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CourseFlow.Converters
{
    public class StringToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string colorString)
            {
                return (SolidColorBrush)new BrushConverter().ConvertFromString(colorString);
            }
            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
