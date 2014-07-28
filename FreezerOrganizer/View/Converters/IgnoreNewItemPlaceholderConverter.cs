using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace FreezerOrganizer.View.Converters
{
    // fixes bug in datagrid when adding a new row
    public class IgnoreNewItemPlaceholderConverter : IValueConverter
    {
        private const string newItemPlaceholderName = "{NewItemPlaceholder}";

        public IgnoreNewItemPlaceholderConverter() { }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value!= null && value.ToString() == newItemPlaceholderName)
            {
                value = DependencyProperty.UnsetValue;
            }
            return value;
        }
    }
}
