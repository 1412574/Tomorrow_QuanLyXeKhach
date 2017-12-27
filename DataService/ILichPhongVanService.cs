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
        int CapNhatThongTinLPV(T lichPhongVan);
        IList<T> XemThongTinLPV();
        T XemThongTinLPV(int id);
        IList<T> XemThongTinLPV(string filter);
    }
}
