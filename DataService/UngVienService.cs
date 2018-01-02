using DAO;
using DataModel;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataService
{
    public class UngVienService : IUngVienService<UngVien>
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        IUnitOfWork unitofWork = new GenericUnitOfWork();

        //cập nhật ứng viên
        public int CapNhatThongTinUV(UngVien ungVien)
        {
            logger.Info("Bat dau cap nhat thong tin ung vien");
            int ret = 0;
            try
            {
                IRepository<UngVien> repository = unitofWork.Repository<UngVien>();
                repository.Update(ungVien);
                unitofWork.SaveChange();
                logger.Info("Status: Success");
                ret = 0;
            }
            catch
            {
                logger.Info("Status: Fail");
                ret = -1;
            }
            return ret;
        }

        //thêm ung viên
        public int ThemUngVien(UngVien ungVien)
        {
            logger.Info("Bat dau them ung vien");
            int ret = 0;
            try
            {
                IRepository<UngVien> repository = unitofWork.Repository<UngVien>();
                repository.Add(ungVien);
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
        
        //tìm nhân viên bằng chuỗi tìm kiếm
         public IList<UngVien> XemThongTinUV(string filter)
        {
            IRepository<UngVien> repository = unitofWork.Repository<UngVien>();
            return repository.GetAll(u => filter == null || (u.maUV.ToString().Contains(filter)) ||
                                                            u.hoTen.Contains(filter)
                                                            ).ToList();
        }
        
        //lấy toàn bộ danh sách nhân viên
        public IList<UngVien> XemThongTinUV()
        {
            IRepository<UngVien> repository = unitofWork.Repository<UngVien>();
            return repository.GetAll().ToList();
        }

        //tìm nhân viên bằng mã nhân viên
        public UngVien XemThongTinUV(int id)
        {
            IRepository<UngVien> repository = unitofWork.Repository<UngVien>();
            return repository.GetById(id);
        }

        //xóa nhân viên
        public int XoaUngVien(int id)
        {
            IRepository<UngVien> repository = unitofWork.Repository<UngVien>();
            logger.Info("Xoa ung vien co ID = " + id.ToString());
            repository.Delete(repository.GetById(id));
            unitofWork.SaveChange();
            return 0;
        }



    }
}
