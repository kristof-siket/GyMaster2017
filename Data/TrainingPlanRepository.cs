namespace Data
{
    using System.Linq;
    using Data.Interfaces;

    /// <summary>
    /// Edzésterv repo
    /// </summary>
    public class TrainingPlanRepository : IRepository<TRAINING_PLAN>
    {
        /// <summary>
        /// Database entities
        /// </summary>
        private GyMasterDBEntities gde;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrainingPlanRepository"/> class.
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
            this.gde.TRAINING_PLAN.Remove(torlendo);
            this.gde.SaveChanges();
        }

        /// <summary>
        /// Gyüjetményként visszaadja az edzésterveket
        /// </summary>
        /// <returns>Az összes edzésterv.</returns>
        public IQueryable<TRAINING_PLAN> GetAll()
        {
            return this.gde.TRAINING_PLAN.AsQueryable<TRAINING_PLAN>();
        }

        /// <summary>
        /// Megadott edzésterv hozzáadása
        /// </summary>
        /// <param name="uj">Hozzáadandó edzésterv</param>
        public void Insert(TRAINING_PLAN uj)
        {
            this.gde.TRAINING_PLAN.Add(uj);
            this.gde.SaveChanges();
        }
    }
}
