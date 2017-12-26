using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public interface ITramXeService<T> where T:class
    {
        int ThemTramXe(T tramXe);
        int XoaTramXe(int id);
        int CapNhatTramXe(T tramXe);
        IList<T> XemTramXe();
        T LayTramXe(int id);
        IList<T> XemHanhTrinhTheoMaTuyen(int id);
    }
}
