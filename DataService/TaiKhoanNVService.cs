using DAO;
using DataModel;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace DataService
{
    public class TaiKhoanNVService : ITaiKhoanNVService<TaiKhoanNV>
    {

        ILogger logger = LogManager.GetCurrentClassLogger();
        IUnitOfWork unitofWork = new GenericUnitOfWork();
        //cập nhật tài khoản
        public int CapNhatTaiKhoan(TaiKhoanNV taiKhoan)
        {
            logger.Info("Start cap nhat tai khoan method");
            int ret = 0;
            try
            {
                IRepository<TaiKhoanNV> repository = unitofWork.Repository<TaiKhoanNV>();
                repository.Update(taiKhoan);
                unitofWork.SaveChange();
                logger.Info("Status: Success");
            }
            catch
            {
                logger.Info("Status: Fail");
                ret = -1;
            }
            return ret;
        }
        //hash 1 chuỗi theo MD5
        public string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        //Thêm tài khoản mới
        public int ThemTaiKhoan(TaiKhoanNV taiKhoan)
        {
            logger.Info("Start them tai khoan method");
            int ret = 0;
            try
            {
                IRepository<TaiKhoanNV> repository = unitofWork.Repository<TaiKhoanNV>();
                repository.Add(taiKhoan);
                unitofWork.SaveChange();
                logger.Info("Status: Success");
            }
            catch
            {
                logger.Info("Status: Fail");
                ret = -1;
            }
            return ret;
        }
        //So khớp 2 chuỗi 
        public bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Lấy danh sách tài khoản
        public IList<TaiKhoanNV> XemTaiKhoan() { 
            IRepository<TaiKhoanNV> repository = unitofWork.Repository<TaiKhoanNV>();
            IList<TaiKhoanNV> listTaiKhoan = new List<TaiKhoanNV>();
            listTaiKhoan = repository.GetAll().ToList();
            return listTaiKhoan;
        }
        //lấy thông tin tài khoản theo mã tài khoản
        public TaiKhoanNV XemTaiKhoanNV(int maTK)
        {
            IRepository<TaiKhoanNV> repository = unitofWork.Repository<TaiKhoanNV>();
            return repository.GetById(maTK);
        }
        //Xóa tài khoản theo mã nhân viên
        public int XoaTaiKhoan(int maTK)
        {
            int ret = 0;
            try
            {
                IRepository<TaiKhoanNV> repository = unitofWork.Repository<TaiKhoanNV>();
                IList<TaiKhoanNV> listVaiTro = new List<TaiKhoanNV>();
                listVaiTro = repository.GetAll().ToList();
                for (int i = 0; i < listVaiTro.Count(); i++)
                {
                    if (listVaiTro[i].maTK == maTK)
                    {
                        repository.Delete(listVaiTro[i]);
                        unitofWork.SaveChange();
                        logger.Info("Status: Success");
                    }
                }
            }
            catch
            {
                logger.Info("Status: Fail");
                ret = -1;
            }
            return ret;
        }
    }
}
