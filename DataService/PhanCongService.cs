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
    public class PhanCongService : IPhanCongService<PhanCong, NhanVien, CongViec>
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        IUnitOfWork unitofWork = new GenericUnitOfWork();

        public int CapNhatThongTinPC(PhanCong phanCong)
        {
            logger.Info("Bat dau cap nhat thong tin phan cong");
            int ret = 0;
            try
            {
                IRepository<PhanCong> repository = unitofWork.Repository<PhanCong>();
                repository.Update(phanCong);
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

        public int ThemPhanCong(PhanCong phanCong)
        {
            logger.Info("Bat dau them cong viec");
            int ret = 0;
            try
            {
                IRepository<PhanCong> repository = unitofWork.Repository<PhanCong>();
                repository.Add(phanCong);
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

        public IList<PhanCong> XemThongTinPC()
        {
            return unitofWork.Repository<PhanCong>().GetAll().ToList();
        }

        public PhanCong XemThongTinPC(int id)
        {
            return unitofWork.Repository<PhanCong>().GetById(id);
        }

        public IList<PhanCong> XemThongTinPC(string filter)
        {
            return unitofWork.Repository<PhanCong>().GetAll(pc => filter == null || (pc.maPC.ToString().Contains(filter)) ||
                                                            pc.maCV.ToString().Contains(filter) ||
                                                            pc.maNV.ToString().Contains(filter) ||
                                                            pc.ngayPC.ToShortDateString().Contains(filter)
                                                            ).ToList(); ;
        }

        public int XoaPhanCong(int id)
        {
            IRepository<PhanCong> repository = unitofWork.Repository<PhanCong>();
            logger.Info("Xoa phan cong co ID = " + id.ToString());
            repository.Delete(repository.GetById(id));
            unitofWork.SaveChange();
            return 0;
        }

        public IList<CongViec> XemCongViec(string filter = null)
        {
            return unitofWork.Repository<CongViec>().GetAll(cv => filter == null || (cv.maCV.ToString().Contains(filter))
                                                            ).ToList(); ;
        }

        public IList<NhanVien> XemNhanVien(string filter = null)
        {
            return unitofWork.Repository<NhanVien>().GetAll(nv => filter == null || (nv.maNV.ToString().Contains(filter))
                                                            ).ToList(); ;
        }

    }
}
