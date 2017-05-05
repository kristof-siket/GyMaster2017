namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Data.Interfaces;

    /// <summary>
    /// Eredmények repo
    /// </summary>
    public class ResultRepository : IRepository<RESULT>
    {
        /// <summary>
        /// Database entities
        /// </summary>
        private GyMasterDBEntities gde;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultRepository"/> class.
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
            this.gde.RESULT.Remove(torlendo);
            this.gde.SaveChanges();
        }

        /// <summary>
        /// Gyüjteményként visszaadja az eredményeket
        /// </summary>
        /// <returns>Az összes RESULT.</returns>
        public IQueryable<RESULT> GetAll()
        {
            return this.gde.RESULT.AsQueryable();
        }

        /// <summary>
        /// Megadott eredmény hozzáadása az adatbázishoz
        /// </summary>
        /// <param name="uj">A beszúrandó result.</param>
        public void Insert(RESULT uj)
        {
            this.gde.RESULT.Add(uj);
            this.gde.SaveChanges();
        }
    }
}
