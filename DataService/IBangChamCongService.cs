using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public interface IBangChamCongService<T>
    {
        int ThemBangChamCong(T bangChamCong);
        int CapNhatThongTinBCC(T bangChamCong);
        int XoaBangChamCong(int maBCC);
        IList<T> XemThongTinBCC();
        T XemThongTinBCC(int id);
        IList<T> XemThongTinBCC(string filter);
    }
}
