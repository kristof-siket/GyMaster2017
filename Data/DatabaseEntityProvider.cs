using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    /// <summary>
    /// Az adatbázis-entitásokat szolgáltató statikus osztály.
    /// </summary>
    public static class DatabaseEntityProvider
    {
        private static GyMasterDBEntities gde;

        public static GyMasterDBEntities GetDatabaseEntities()
        {
            if (gde == null)
                gde = new GyMasterDBEntities();
            return gde;
        }
    }
}
