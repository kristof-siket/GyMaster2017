namespace Data
{
    using System.Linq;
    using Data.Interfaces;

    /// <summary>
    /// Edzőterem repo
    /// </summary>
    public class GymRepository : IRepository<GYM>
    {
        /// <summary>
        /// Database entities
        /// </summary>
        private GyMasterDBEntities gde;

        /// <summary>
        /// Initializes a new instance of the <see cref="GymRepository"/> class.
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
            this.gde.GYM.Remove(torlendo);
            this.gde.SaveChanges();
        }

        /// <summary>
        /// Gyüjteményként visszaadja a konditermeket
        /// </summary>
        /// <returns>Az összes konditerem.</returns>
        public IQueryable<GYM> GetAll()
        {
            return this.gde.GYM.AsQueryable<GYM>();
        }

        /// <summary>
        /// Új konditerem beillesztése az adatbázisba
        /// </summary>
        /// <param name="uj">Beszúrandó konditerem.</param>
        public void Insert(GYM uj)
        {
            this.gde.GYM.Add(uj);
            this.gde.SaveChanges();
        }
    }
}
