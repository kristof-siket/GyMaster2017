using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Logic
    {
        GyMasterDBEntities gde;
        public static GyMasterRepository repo { get; set; }
        public Logic()
        {
            gde = new GyMasterDBEntities();
            repo = new GyMasterRepository(gde);
        }

        public static void addNewMember(ATHLETE at,GyMasterRepository repo)
        {
            repo.Insert(at);
        }

        /// <summary>
        /// Egyszerű, gagyi login validáció.
        /// </summary>
        /// <param name="actUser">Ez lesz majd az inputon beadott név.</param>
        /// <param name="actPasswd">Input jelszó.</param>
        /// <returns></returns>
        public bool LoginEllenorzes(string actUser, string actPasswd)
        {
            foreach (ATHLETE a in repo.GetAll())
                if (a.NAME == actUser && a.PASSWORD == actPasswd)
                    return true;
            return false;
        }
    }
}
