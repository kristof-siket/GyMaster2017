namespace Data
{
    using System.Linq;
    using Data.Interfaces;

    public class AthleteRepository : IRepository<ATHLETE>
    {
        /// <summary>
        /// Database entities
        /// </summary>
        private GyMasterDBEntities gde;

        /// <summary>
        /// Initializes a new instance of the <see cref="AthleteRepository"/> class.
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
            this.gde.ATHLETE.Remove(torlendo);
            this.gde.SaveChanges();
        }

        /// <summary>
        /// Összes sportoló visszaadás IQueryable gyüjteményként
        /// </summary>
        /// <returns>Az összes sportoló.</returns>
        public IQueryable<ATHLETE> GetAll()
        {
            return this.gde.ATHLETE.AsQueryable<ATHLETE>();
        }

        /// <summary>
        /// Megadott sportoló hozzáadása az adatbázishoz
        /// </summary>
        /// <param name="uj">Hozzáadandó sportoló</param>
        public void Insert(ATHLETE uj)
        {
            this.gde.ATHLETE.Add(uj);
            this.gde.SaveChanges();
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
}
