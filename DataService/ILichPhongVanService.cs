using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public interface ILichPhongVanService<T> where T : class
    {
        int ThemLichPhongVan(T lichPhongVan);
        int XoaLichPhongVan(int id);
        int CapNhatLichPhongVan(T lichPhongVan);
        IList<T> XemLichPhongVan();
        T XemLichPhongVan(int id);
    }
}
