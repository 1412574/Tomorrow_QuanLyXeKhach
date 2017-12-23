using System.Collections.Generic;

namespace DataService
{
    public interface ITrangThaiUVService<T> where T : class
    {
        int ThemTrangThaiUV(T trangThai);
        int XoaTrangThaiUV(int id);
        int CapNhatTrangThaiUV(T trangThai);
        IList<T> XemTrangThaiUV();
        T XemTrangThaiUV(int id);
    }
}
