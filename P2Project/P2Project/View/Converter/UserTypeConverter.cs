using P2Project.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace P2Project.View.Converter
{
    public class UserTypeToDanishConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            UserType type = (UserType)value;
            string danish = "Ukendt";
            if (type == UserType.Pupil)
                danish = "Elev";
            else if (type == UserType.Teacher)
                danish = "Underviser";
            else if (type == UserType.Admin)
                danish = "Admin";
            return "Brugertype: " + danish;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
