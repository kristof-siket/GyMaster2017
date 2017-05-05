namespace Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Irepository interface
    /// </summary>
    /// <typeparam name="T">Generikus T</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Hozzáadás
        /// </summary>
        /// <param name="uj">Hozzáadandó elem</param>
        void Insert(T uj);

        /// <summary>
        /// Törlés
        /// </summary>
        /// <param name="torlendo">Törlendő elem</param>
        void Delete(T torlendo);

        /// <summary>
        /// Gyüjetményként visszaadja az összeset
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();
    }
}
