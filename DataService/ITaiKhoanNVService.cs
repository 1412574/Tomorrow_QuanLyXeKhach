using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public interface ITaiKhoanNVService<T> where T : class
    {
        int ThemTaiKhoan(T taiKhoan);
        int CapNhatTaiKhoan(T taiKhoan);
        T XemTaiKhoanNV(int maTK);
        IList<T> XemTaiKhoan();
        int XoaTaiKhoan(int maTK);
        string  GetMd5Hash(MD5 md5Hash, string input);
        bool VerifyMd5Hash(MD5 md5Hash, string input, string hash);

    }
}
