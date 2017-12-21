using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public interface INhanVienService<T> where T : class
    {
        IEnumerable<T> XemNhanVien();
        int ThemNhanVien(T nhanVien);
        T XemNhanVienNV(int maNV);
        int XoaNhanVien(int maNV);
        int CapNhatNhanVien(T nhanVien);
    }
}
