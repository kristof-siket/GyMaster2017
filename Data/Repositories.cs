using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{

    public class AthleteRepository : IRepository<ATHLETE>
    {
        GyMasterDBEntities gde;

        public AthleteRepository()
        {
            this.gde = DatabaseEntityProvider.GetDatabaseEntities();
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

        public ATHLETE GetAthleteByName(string name)
        {
            var res = from x in this.GetAll()
                      where x.NAME == name
                      select x;
            return res.First();
        }
    }

    public class GymRepository : IRepository<GYM>
    {
        GyMasterDBEntities gde;

        public GymRepository()
        {
            this.gde = DatabaseEntityProvider.GetDatabaseEntities();
        }

        public void Delete(GYM torlendo)
        {
            gde.GYM.Remove(torlendo);
            gde.SaveChanges();
        }

        public IQueryable<GYM> GetAll()
        {
            return gde.GYM.AsQueryable<GYM>();
        }

        public void Insert(GYM uj)
        {
            gde.GYM.Add(uj);
            gde.SaveChanges();
        }
    }

    public class TrainingPlanRepository : IRepository<TRAINING_PLAN>
    {
        GyMasterDBEntities gde;

        public TrainingPlanRepository()
        {
            this.gde = DatabaseEntityProvider.GetDatabaseEntities();
        }

        public void Delete(TRAINING_PLAN torlendo)
        {
            gde.TRAINING_PLAN.Remove(torlendo);
            gde.SaveChanges();
        }

        public IQueryable<TRAINING_PLAN> GetAll()
        {
            return gde.TRAINING_PLAN.AsQueryable<TRAINING_PLAN>();
        }

        public void Insert(TRAINING_PLAN uj)
        {
            gde.TRAINING_PLAN.Add(uj);
            gde.SaveChanges();
        }
    }

    public class ExerciseRepository : IRepository<EXERCISE>
    {
        GyMasterDBEntities gde;

        public ExerciseRepository()
        {
            this.gde = DatabaseEntityProvider.GetDatabaseEntities();
        }

        public void Delete(EXERCISE torlendo)
        {
            gde.EXERCISE.Remove(torlendo);
            gde.SaveChanges();
        }

        public IQueryable<EXERCISE> GetAll()
        {
            return gde.EXERCISE.AsQueryable<EXERCISE>();
        }

        public void Insert(EXERCISE uj)
        {
            gde.EXERCISE.Add(uj);
            gde.SaveChanges();
        }

        public EXERCISE GetExerciseByName(string name)
        {
            var res = from x in this.GetAll()
                      where x.NAME == name
                      select x;
            return res.First();
        }
    }
    public class ResultRepository : IRepository<RESULT>
    {
        GyMasterDBEntities gde;

        public ResultRepository()
        {
            this.gde = DatabaseEntityProvider.GetDatabaseEntities();
        }

        public void Delete(RESULT torlendo)
        {
            gde.RESULT.Remove(torlendo);
            gde.SaveChanges();
        }

        public IQueryable<RESULT> GetAll()
        {
            return gde.RESULT.AsQueryable();
        }

        public void Insert(RESULT uj)
        {
            gde.RESULT.Add(uj);
            gde.SaveChanges();
        }
    }
}
