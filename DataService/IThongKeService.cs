using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public interface IThongKeService<T> where T : class
    {
        int ThemThongKe(T thongKe);
        IList<ThongKe> XemThongKe(T thongKe);
        int XoaThongKe(int maPB);
        int CapNhatThongKe(T thongKe);
        ThongKe GetThongKe(int id);
    }
}
