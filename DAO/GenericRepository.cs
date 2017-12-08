using DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private QLXeKhachContext _context;
        public GenericRepository(QLXeKhachContext context)
        {
            _context = context;            
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public T Get(Func<T, bool> predicate)
        {
            return _context.Set<T>().First(predicate);
        }
        public virtual T GetByID(int id)
        {
            return _context.Set<T>().Find(id);
        }
        public IEnumerable<T> GetAll(Func<T, bool> predicate = null)
        {
            if (predicate != null)
            {
                _context.Set<T>().Where(predicate);
            }
            return _context.Set<T>().AsEnumerable<T>();
        }
    }
}
