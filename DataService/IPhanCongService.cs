using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public interface IPhanCongService<PC, NV, CV> 
    {
        int ThemPhanCong(PC phanCong);
        int CapNhatThongTinPC(PC phanCong);
        int XoaPhanCong(int maCV);
        IList<PC> XemThongTinPC();
        PC XemThongTinPC(int id);
        IList<PC> XemThongTinPC(string filter);

        IList<NV> XemNhanVien(string filter = null);
        IList<CV> XemCongViec(string filter = null);
    }
}
