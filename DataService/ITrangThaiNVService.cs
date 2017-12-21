using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public interface ITrangThaiNVService<T> where T : class
    {
        int ThemTrangThaiNV(T trangThai);
        int CapNhatTrangThaiNV(T trangThai);
        T XemTrangThaiNV(int maTT);
        IList<T> XemTrangThai();
        int XoaTrangThaiNV(int maTT);
    }
}
