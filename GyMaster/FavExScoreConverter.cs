namespace GyMaster
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Data;
    using Data;

    /// <summary>
    /// Kedvenc gyakorlat alapján visszaadja a hozzá tartozó legjobb pontszámot is
    /// </summary>
    public class FavExScoreConverter : IValueConverter
    {
        /// <summary>
        /// Kedvenc gyakorlatot konvertálja hozzá tartozó eredményhez
        /// </summary>
        /// <param name="value">érték objektum</param>
        /// <param name="targetType">cél típus</param>
        /// <param name="parameter">paraméter objektum</param>
        /// <param name="culture">culture info</param>
        /// <returns>A kedvenc gyakorlathoz tartozó eredmény.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ViewModel vm = ViewModel.Get();
            var res = from x in vm.BL.GetAthleteRepository().GetAll()
                      where x.NAME == (string)value
                      select x;
            ATHLETE at = res.First();
            var res2 = from x in vm.BL.GetResultRepository().GetAll()
                       where x.ATHLETE.NAME == at.NAME && x.EXERCISE.NAME == at.FAVOURITE_EX.NAME
                       select x;
            int i = 0;
            int sumKg = 0;

            if (at.FAVOURITE_EX != null)
            {
                if (res2.Count() != 0)
                {
                    foreach (var item in res2)
                    {
                        sumKg += (int)item.RES_KG;
                        i++;
                    }

                    return "A kedvenc gyakorlata:" + res2.First().EXERCISE.NAME + " és hozzá tartozó eredménye:" + (sumKg / i);
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

        /// <summary>
        /// Nincs használva
        /// </summary>
        /// <param name="value">érték objektum</param>
        /// <param name="targetType">cél típus</param>
        /// <param name="parameter">paraméter objektum</param>
        /// <param name="culture">culture info</param>
        /// <returns>NotImplementedException()</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
