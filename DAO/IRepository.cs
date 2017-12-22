using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface IRepository<T> where T:class
    {
        IEnumerable<T> GetAll(Func<T, bool> predicate = null);
        T GetById(object id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
