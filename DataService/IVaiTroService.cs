using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public interface IVaiTroService<T> where T : class
    {
        int ThemVaiTro(T vaiTro);
        int CapNhatVaiTro(T vaiTro);
        T XemVaiTroNV(int id);
        IList<T> XemVaiTro();
        int XoaVaiTro(int maVT);
    }
}
