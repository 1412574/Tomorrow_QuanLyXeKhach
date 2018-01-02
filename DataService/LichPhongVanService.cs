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
    public class LichPhongVanService : ILichPhongVanService<LichPhongVan>
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        IUnitOfWork unitofWork = new GenericUnitOfWork();

        //cập nhật lịch phỏng vấn theo mã phỏng vấn
        public int CapNhatThongTinLPV(LichPhongVan lichPhongVan)
        {
            logger.Info("Bat dau cap nhat thong tin lich phong van");
            int ret = 0;
            try
            {
                IRepository<LichPhongVan> repository = unitofWork.Repository<LichPhongVan>();
                repository.Update(lichPhongVan);
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

        //thêm lịch phỏng vấn
        public int ThemLichPhongVan(LichPhongVan lichPhongVan)
        {
            logger.Info("Bat dau them ung vien");
            int ret = 0;
            try
            {
                IRepository<LichPhongVan> repository = unitofWork.Repository<LichPhongVan>();
                repository.Add(lichPhongVan);
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

        //lấy toàn bộ danh sách lịch phỏng vấn
        public IList<LichPhongVan> XemThongTinLPV()
        {
            IRepository<LichPhongVan> repository = unitofWork.Repository<LichPhongVan>();
            return repository.GetAll().ToList();
        }

        //tìm lịch phognr vấn bằng mã lịch phỏng vấn
        public LichPhongVan XemThongTinLPV(int id)
        {
            IRepository<LichPhongVan> repository = unitofWork.Repository<LichPhongVan>();
            return repository.GetById(id);
        }

        //tìm kiếm lịch phỏng vấn bằng chuỗi tìm kiếm
        public IList<LichPhongVan> XemThongTinLPV(string filter)
        {
            IRepository<LichPhongVan> repository = unitofWork.Repository<LichPhongVan>();
            return repository.GetAll(u => filter == null || (u.maLPV.ToString().Contains(filter))
                                                            ).ToList();
        }

        //xóa lịch phỏng vấn
        public int XoaLichPhongVan(int id)
        {
            IRepository<LichPhongVan> repository = unitofWork.Repository<LichPhongVan>();
            logger.Info("Xoa lich phong vans co ID = " + id.ToString());
            repository.Delete(repository.GetById(id));
            unitofWork.SaveChange();
            return 0;
        }
    }
}
