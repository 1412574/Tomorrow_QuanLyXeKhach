using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public interface IChuyenXeService<T> where T:class
    {
        int ThemChuyenXe(T chuyenXe);
        int XoaChuyenXe(int id);
        int CapNhatChuyenXe(T chuyenXe);
        IList<T> XemChuyenXe();
        T LayChuyenXe(int id);
    }
}
