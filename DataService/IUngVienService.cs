using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public interface IUngVienService<T> where T : class
    {
        int ThemUngVien(T ungVien);
        int XoaUngVien(int id);
        int CapNhatThongTinUV(T ungVien);
        IList<T> XemThongTinUV();
        T XemThongTinUV(int id);
        IList<T> XemThongTinUV(string filter);
    }
}
