using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Data;
using System.Text.RegularExpressions;

namespace Prdc.Conference.Model
{

    public class CleanHtmlValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return CleanHtmlValueConverter.Convert(value as string);
        }

        public object ConvertBack(object value, Type targetType, object parameter,
        System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static string Convert(string input)
        {

            // Remove HTML tags and empty newlines and spaces
            string returnString = Regex.Replace(input, @"< .*?>", "");
            returnString = Regex.Replace(returnString, @"n+s+", "nn");

            // Decode HTML entities
            returnString = HttpUtility.HtmlDecode(returnString);

            return returnString;
        }

    }

}
