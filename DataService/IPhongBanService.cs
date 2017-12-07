using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public interface IPhongBanService<T> where T:class
    {
        int ThemPhongBan(T phongBan);
    }
}
