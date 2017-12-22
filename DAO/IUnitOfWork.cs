using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<T> Repository<T>() where T : class;
        void SaveChange();
        void Dispose(bool disposing);
        new void Dispose();
    }
}
