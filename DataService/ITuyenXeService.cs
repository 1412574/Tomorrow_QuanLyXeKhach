using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public interface ITuyenXeService<T> where T : class
    {
        int ThemTuyenXe(T tuyenXe);
        int XoaTuyenXe(int id);
        int CapNhatTuyenXe(T tuyenXe);
        IList<T> XemTuyenXe();
        T LayTuyenXe(int id);
    }
}
