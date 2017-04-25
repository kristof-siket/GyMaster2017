using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class GyMasterRepository : IRepository<ATHLETE>
    {
        GyMasterDBEntities gde;
        public GyMasterRepository(GyMasterDBEntities gde)
        {
            this.gde = gde;
        }
        public void Delete(ATHLETE torlendo)
        {
            gde.ATHLETE.Remove(torlendo);
            gde.SaveChanges();
        }

        public IQueryable<ATHLETE> GetAll()
        {
            return gde.ATHLETE.AsQueryable<ATHLETE>();
        }

        public void Insert(ATHLETE uj)
        {
            gde.ATHLETE.Add(uj);
            gde.SaveChanges();
        }

        
    }
}
