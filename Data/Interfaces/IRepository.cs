using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IRepository<T>
    {
        void Insert(T uj);
        void Delete(T torlendo);
        IQueryable<T> GetAll();
    }
}
