using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public interface IHanhTrinhService<T> where T:class
    {
        int ThemHanhTrinh(T hanhTrinh);
        int XoaHanhTrinh(int id);
        int CapNhatHanhTrinh(T hanhTrinh);
        IList<T> XemHanhTrinh();
        T LayHanhTrinh(int id);
        
    }
}
