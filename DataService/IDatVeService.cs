using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public interface IDatVeService<T> where T:class
    {
        int themDatVe(T t);
        IList<T> xemDatVe();

        T layDatVe(int id);
        int xoaDatVe(int id);
        int capnhatDatVe(T t);
    }
}

