using Monitor_kl2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Monitor_kl2
{
    public class HostStatusToColorConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Color color = Colors.White;

            switch ((HostModel.StatusEnum)value)
            {
                case HostModel.StatusEnum.Activated:
                    color = Colors.Green;
                    break;
                case HostModel.StatusEnum.Disabled:
                    color = Colors.Red;
                    break;
                case HostModel.StatusEnum.AddedToList:
                    color = Colors.LightSalmon;
                    break;
            }

            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
