using System;
using System.Windows.Data;

namespace Controls
{
    class EnumValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || parameter == null)
                return false;
            return value.ToString() == parameter.ToString();
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || parameter == null)
                return null;
            var rtnValue = parameter.ToString();
            try
            {
                object returnEnum = Enum.Parse(targetType, rtnValue);
                return returnEnum;
            }
            catch
            {
                return null;
            }
        }
    }
}
