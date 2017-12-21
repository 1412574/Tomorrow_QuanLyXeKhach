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
    public class TrangThaiNVService:ITrangThaiNVService<TrangThaiNV>
    {

        ILogger logger = LogManager.GetCurrentClassLogger();
        IUnitOfWork unitofWork = new GenericUnitOfWork();

        //cập nhật vai trò của nhân viên
        public int CapNhatTrangThaiNV(TrangThaiNV trangThaiNV)
        {
            logger.Info("Start cap nhat trang thai method");
            int ret = 0;
            try
            {
                IRepository<TrangThaiNV> repository = unitofWork.Repository<TrangThaiNV>();
                repository.Update(trangThaiNV);
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
        //Thêm trạng thái mới
        public int ThemTrangThaiNV(TrangThaiNV trangThaiNV)
        {
            logger.Info("Start them trang thai method");
            int ret = 0;
            try
            {
                IRepository<TrangThaiNV> repository = unitofWork.Repository<TrangThaiNV>();
                repository.Add(trangThaiNV);
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
        //Lấy danh sách trạng thái
        public IList<TrangThaiNV> XemTrangThai()
        {
            IRepository<TrangThaiNV> repository = unitofWork.Repository<TrangThaiNV>();
            IList<TrangThaiNV> listtrangThai = new List<TrangThaiNV>();
            listtrangThai = repository.GetAll().ToList();
            return listtrangThai;
        }
        //Lấy một trạng thái bất kỳ
        public TrangThaiNV XemTrangThaiNV(int maTT)
        {
            IRepository<TrangThaiNV> repository = unitofWork.Repository<TrangThaiNV>();
            return repository.GetById(maTT);
        }
        //xóa một trạng thái
        public int XoaTrangThaiNV(int maTT)
        {
            int ret = 0;
            try
            {
                IRepository<TrangThaiNV> repository = unitofWork.Repository<TrangThaiNV>();
                IList<TrangThaiNV> listTrangThai = new List<TrangThaiNV>();
                listTrangThai = repository.GetAll().ToList();
                for (int i = 0; i < listTrangThai.Count(); i++)
                {
                    if (listTrangThai[i].maTT == maTT)
                    {
                        repository.Delete(listTrangThai[i]);
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
