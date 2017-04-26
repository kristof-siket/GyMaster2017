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
        static GyMasterDBEntities gde;
        private AthleteRepository athleteRepo;

        public AthleteRepository GetAthleteRepository()
        {
            if (athleteRepo == null)
                athleteRepo = new AthleteRepository();
            return athleteRepo;
        }

        public static GyMasterDBEntities GetDbEntities()
        {
            if (gde == null)
                gde = new GyMasterDBEntities();
            return gde;
        }

        public Logic()
        {
            gde = new GyMasterDBEntities() ;
            athleteRepo = new AthleteRepository();
        }

        public static void addNewMember(ATHLETE at,AthleteRepository repo)
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
            foreach (ATHLETE a in athleteRepo.GetAll())
                if (a.NAME == actUser && a.PASSWORD == actPasswd)
                    return true;
            return false;
        }
    }
}
