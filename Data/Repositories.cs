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
        /// <summary>
        /// Database entities
        /// </summary>
        GyMasterDBEntities gde;

        /// <summary>
        /// AthleteRepository konstruktor
        /// </summary>
        public AthleteRepository()
        {
            this.gde = DatabaseEntityProvider.GetDatabaseEntities();
        }

        /// <summary>
        /// Megadott sportoló törlése
        /// </summary>
        /// <param name="torlendo">törlendő sportoló</param>
        public void Delete(ATHLETE torlendo)
        {
            gde.ATHLETE.Remove(torlendo);
            gde.SaveChanges();
        }

        /// <summary>
        /// Összes sportoló visszaadás IQueryable gyüjteményként
        /// </summary>
        /// <returns></returns>
        public IQueryable<ATHLETE> GetAll()
        {
            return gde.ATHLETE.AsQueryable<ATHLETE>();
        }

        /// <summary>
        /// Megadott sportoló hozzáadása az adatbázishoz
        /// </summary>
        /// <param name="uj">Hozzáadandó sportoló</param>
        public void Insert(ATHLETE uj)
        {
            gde.ATHLETE.Add(uj);
            gde.SaveChanges();
        }

        /// <summary>
        /// Sportoló lekérdezése név szerint
        /// </summary>
        /// <param name="name">sportoló neve</param>
        /// <returns>Sportoló</returns>
        public ATHLETE GetAthleteByName(string name)
        {
            var res = from x in this.GetAll()
                      where x.NAME == name
                      select x;
            return res.First();
        }
    }

    /// <summary>
    /// Edzőterem repo
    /// </summary>
    public class GymRepository : IRepository<GYM>
    {
        /// <summary>
        /// Database entities
        /// </summary>
        GyMasterDBEntities gde;

        /// <summary>
        /// Gym repo konstruktor
        /// </summary>
        public GymRepository()
        {
            this.gde = DatabaseEntityProvider.GetDatabaseEntities();
        }

        /// <summary>
        /// Megadott konditerem törlése
        /// </summary>
        /// <param name="torlendo">törlendő konditerem</param>
        public void Delete(GYM torlendo)
        {
            gde.GYM.Remove(torlendo);
            gde.SaveChanges();
        }

        /// <summary>
        /// Gyüjteményként visszaadja a konditermeket
        /// </summary>
        /// <returns></returns>
        public IQueryable<GYM> GetAll()
        {
            return gde.GYM.AsQueryable<GYM>();
        }

        /// <summary>
        /// Új konditerem beillesztése az adatbázisba
        /// </summary>
        /// <param name="uj"></param>
        public void Insert(GYM uj)
        {
            gde.GYM.Add(uj);
            gde.SaveChanges();
        }
    }

    /// <summary>
    /// Edzésterv repo
    /// </summary>
    public class TrainingPlanRepository : IRepository<TRAINING_PLAN>
    {
        /// <summary>
        /// Database entities
        /// </summary>
        GyMasterDBEntities gde;

        /// <summary>
        /// Trainingplan repo konstruktora
        /// </summary>
        public TrainingPlanRepository()
        {
            this.gde = DatabaseEntityProvider.GetDatabaseEntities();
        }

        /// <summary>
        /// Megadott edzésterv törlése
        /// </summary>
        /// <param name="torlendo">törlendő edzésterv</param>
        public void Delete(TRAINING_PLAN torlendo)
        {
            gde.TRAINING_PLAN.Remove(torlendo);
            gde.SaveChanges();
        }

        /// <summary>
        /// Gyüjetményként visszaadja az edzésterveket
        /// </summary>
        /// <returns></returns>
        public IQueryable<TRAINING_PLAN> GetAll()
        {
            return gde.TRAINING_PLAN.AsQueryable<TRAINING_PLAN>();
        }

        /// <summary>
        /// Megadott edzésterv hozzáadása
        /// </summary>
        /// <param name="uj">Hozzáadandó edzésterv</param>
        public void Insert(TRAINING_PLAN uj)
        {
            gde.TRAINING_PLAN.Add(uj);
            gde.SaveChanges();
        }
    }

    /// <summary>
    /// Gyakorlatok repo
    /// </summary>
    public class ExerciseRepository : IRepository<EXERCISE>
    {
        /// <summary>
        /// Database entities
        /// </summary>
        GyMasterDBEntities gde;

        /// <summary>
        /// Exercise repo kontstruktora
        /// </summary>
        public ExerciseRepository()
        {
            this.gde = DatabaseEntityProvider.GetDatabaseEntities();
        }

        /// <summary>
        /// Megadott gyakorlat törlése
        /// </summary>
        /// <param name="torlendo">törlendő gyakorlat</param>
        public void Delete(EXERCISE torlendo)
        {
            gde.EXERCISE.Remove(torlendo);
            gde.SaveChanges();
        }

        /// <summary>
        /// Gyüjteményként visszaadja a gyakorlatokat
        /// </summary>
        /// <returns></returns>
        public IQueryable<EXERCISE> GetAll()
        {
            return gde.EXERCISE.AsQueryable<EXERCISE>();
        }

        /// <summary>
        /// Megadott gyakorlat hozzáadása az adatbázishoz
        /// </summary>
        /// <param name="uj">hozzáaadando gyakorlat</param>
        public void Insert(EXERCISE uj)
        {
            gde.EXERCISE.Add(uj);
            gde.SaveChanges();
        }

        /// <summary>
        /// Gyakorlat visszaadása a megadott név szerint
        /// </summary>
        /// <param name="name">Gyakorlat neve</param>
        /// <returns></returns>
        public EXERCISE GetExerciseByName(string name)
        {
            var res = from x in this.GetAll()
                      where x.NAME == name
                      select x;
            return res.First();
        }
    }

    /// <summary>
    /// Eredmények repo
    /// </summary>
    public class ResultRepository : IRepository<RESULT>
    {
        /// <summary>
        /// Database entities
        /// </summary>
        GyMasterDBEntities gde;

        /// <summary>
        /// Result repo konstruktora
        /// </summary>
        public ResultRepository()
        {
            this.gde = DatabaseEntityProvider.GetDatabaseEntities();
        }

        /// <summary>
        /// Megadott eredmény törlése
        /// </summary>
        /// <param name="torlendo">törlendő eredmény</param>
        public void Delete(RESULT torlendo)
        {
            gde.RESULT.Remove(torlendo);
            gde.SaveChanges();
        }

        /// <summary>
        /// Gyüjteményként visszaadja az eredményeket
        /// </summary>
        /// <returns></returns>
        public IQueryable<RESULT> GetAll()
        {
            return gde.RESULT.AsQueryable();
        }

        /// <summary>
        /// Megadott eredmény hozzáadása az adatbázishoz
        /// </summary>
        /// <param name="uj"></param>
        public void Insert(RESULT uj)
        {
            gde.RESULT.Add(uj);
            gde.SaveChanges();
        }
    }
}
