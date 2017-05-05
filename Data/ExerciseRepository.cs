namespace Data
{
    using System.Linq;
    using Data.Interfaces;

    /// <summary>
    /// Gyakorlatok repo
    /// </summary>
    public class ExerciseRepository : IRepository<EXERCISE>
    {
        /// <summary>
        /// Database entities
        /// </summary>
        private GyMasterDBEntities gde;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExerciseRepository"/> class.
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
            this.gde.EXERCISE.Remove(torlendo);
            this.gde.SaveChanges();
        }

        /// <summary>
        /// Gyüjteményként visszaadja a gyakorlatokat
        /// </summary>
        /// <returns>Az összes gyakorlat.</returns>
        public IQueryable<EXERCISE> GetAll()
        {
            return this.gde.EXERCISE.AsQueryable<EXERCISE>();
        }

        /// <summary>
        /// Megadott gyakorlat hozzáadása az adatbázishoz
        /// </summary>
        /// <param name="uj">hozzáaadando gyakorlat</param>
        public void Insert(EXERCISE uj)
        {
            this.gde.EXERCISE.Add(uj);
            this.gde.SaveChanges();
        }

        /// <summary>
        /// Gyakorlat visszaadása a megadott név szerint
        /// </summary>
        /// <param name="name">Gyakorlat neve</param>
        /// <returns>A keresett nevű gyakorlat.</returns>
        public EXERCISE GetExerciseByName(string name)
        {
            var res = from x in this.GetAll()
                      where x.NAME == name
                      select x;
            return res.First();
        }
    }
}
