using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public interface IBaoCaoService<T> where T : class
    {
        int ThemBaoCao(T baoCao);
        IList<BaoCao> XemBaoCao(T baoCao);
        int XoaBaoCao(int maBaoCao);
        int CapNhatBaoCao(T baoCao);
        BaoCao GetBaoCao(int id);
    }
}
