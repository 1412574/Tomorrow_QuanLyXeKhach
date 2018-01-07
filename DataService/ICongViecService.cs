using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public interface ICongViecService<T> where T : class
    {
        int ThemCongViec(T congViec);
        int CapNhatThongTinCV(T congViec);
        int XoaCongViec(int maCV);
        IList<T> XemThongTinCV();
        T XemThongTinCV(int id);
        IList<T> XemThongTinCV(string filter);
    }
}
