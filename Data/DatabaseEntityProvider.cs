namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Az adatbázis-entitásokat szolgáltató statikus osztály.
    /// </summary>
    public static class DatabaseEntityProvider
    {
        private static GyMasterDBEntities gde;

        /// <summary>
        /// Ezen keresztül kell elérni a gde-t.
        /// </summary>
        /// <returns>GyMasterDBEntities példány.</returns>
        public static GyMasterDBEntities GetDatabaseEntities()
        {
            if (gde == null)
            {
                gde = new GyMasterDBEntities();
            }

            return gde;
        }
    }
}
