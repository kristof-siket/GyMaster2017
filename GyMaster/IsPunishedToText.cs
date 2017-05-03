using Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GyMaster
{
    /// <summary>
    /// Büntetés szöveggé konvertálása
    /// </summary>
    public class IsPunishedToText : IValueConverter
    {
        /// <summary>
        /// Attól függően hogy meg van e büntetve visszaad szöveget
        /// </summary>
        /// <param name="value">érték objektum</param>
        /// <param name="targetType">cél típus</param>
        /// <param name="parameter">paraméter objektum</param>
        /// <param name="culture">culture info</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
                        
            if ((bool)value)
            {
                return "A sportoló meg van büntetve!";
            }
            else
            {
                return "A sportoló nincs megbüntetve";
            }
        }

        /// <summary>
        /// Nincs használva
        /// </summary>
        /// <param name="value">érték objektum</param>
        /// <param name="targetType">cél típus</param>
        /// <param name="parameter">paraméter objektum</param>
        /// <param name="culture">culture info</param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
