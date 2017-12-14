using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public interface IKhachHangService<T> where T:class
    {
        int themKhachHang(T t);
        IList<T> xemKhachHang();

        T layKhachHang(int id);
        int xoaKhachHang(int id);
        int capnhatKhachHang(T t);
    }
}
