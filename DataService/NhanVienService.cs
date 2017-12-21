using DAO;
using DataModel;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public class NhanVienService:INhanVienService<NhanVien>
    {

        ILogger logger = LogManager.GetCurrentClassLogger();
        IUnitOfWork unitofWork = new GenericUnitOfWork();

        public int CapNhatNhanVien(NhanVien nhanVien)
        {
            logger.Info("Start cap nhat nhan vien method");
            int ret = 0;
            try
            {
                IRepository<NhanVien> repository = unitofWork.Repository<NhanVien>();
                repository.Update(nhanVien);
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

        //thêm nhân viên mới
        public int ThemNhanVien(NhanVien nhanVien)
        {
            logger.Info("Start them phong ban method");
            int ret = 0;
            try
            {
                IRepository<NhanVien> repository = unitofWork.Repository<NhanVien>();
                repository.Add(nhanVien);
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
        //xem thông tin một nhân viên theo mã nhân viên
        public NhanVien XemNhanVienNV(int maNV)
        {
            IRepository<NhanVien> repository = unitofWork.Repository<NhanVien>();
            return repository.GetById(maNV);
        }
        //Xóa thông tin nhân viên
        public int XoaNhanVien(int maNV)
        {

            int ret = 0;
            try
            {
                IRepository<NhanVien> repository = unitofWork.Repository<NhanVien>();
                IList<NhanVien> listNhanVien = new List<NhanVien>();
                listNhanVien = repository.GetAll().ToList();
                for (int i = 0; i < listNhanVien.Count(); i++)
                {
                    if (listNhanVien[i].maNV == maNV)
                    {
                        repository.Delete(listNhanVien[i]);
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
        //Lấy danh sách nhân viên
        IEnumerable<NhanVien> INhanVienService<NhanVien>.XemNhanVien()
        {
            IRepository<NhanVien> repository = unitofWork.Repository<NhanVien>();
            return repository.GetAll();
        }
    }
}
