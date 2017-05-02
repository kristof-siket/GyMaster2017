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
    public class FavExScoreConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ViewModel VM = ViewModel.Get();
            var res = from x in VM.BL.GetAthleteRepository().GetAll()
                      where x.NAME == (string)value
                      select x;
            ATHLETE at = res.First();
            var res2 = from x in VM.BL.GetResultRepository().GetAll()
                       where x.ATHLETE.NAME == at.NAME && x.EXERCISE.NAME == at.FAVOURITE_EX.NAME
                       select x;
            int i = 0;
            int sumKg = 0;

            if (at.FAVOURITE_EX!=null)
            {
                if (res2.Count() != 0)
                {
                    foreach (var item in res2)
                    {
                        sumKg += (int)item.RES_KG;
                        i++;
                    }
                    return "A kedvenc gyakorlata:" + res2.First().EXERCISE.NAME + " és hozzá tartozó eredménye:" + sumKg / i;
                }
                else
                {
                    return "A kedvenc gyakorlata: " + at.FAVOURITE_EX.NAME + " de eredménye még nincs hozzá.";
                }
            }
            else
            {
                return "Nincs kedvenc gyakorlata";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
