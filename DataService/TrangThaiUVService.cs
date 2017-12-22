using DAO;
using DataModel;
using NLog;
using System.Collections.Generic;
using System.Linq;

namespace DataService
{
    public class TrangThaiUVService : ITrangThaiUVService<TrangThaiUV>
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        IUnitOfWork unitofWork = new GenericUnitOfWork();
        public int CapNhatTrangThaiUV(TrangThaiUV trangThai)
        {
            logger.Info("Bat dau cap nhat thong tin trang thai ung vien");
            int ret = 0;
            try
            {
                IRepository<TrangThaiUV> repository = unitofWork.Repository<TrangThaiUV>();
                repository.Update(trangThai);
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

        public int ThemTrangThaiUV(TrangThaiUV trangThai)
        {
            logger.Info("Bat dau them trang thai ung vien");
            int ret = 0;
            try
            {
                IRepository<TrangThaiUV> repository = unitofWork.Repository<TrangThaiUV>();
                repository.Add(trangThai);
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

        public IList<TrangThaiUV> XemTrangThaiUV()
        {
            IRepository<TrangThaiUV> repository = unitofWork.Repository<TrangThaiUV>();
            return repository.GetAll().ToList();
        }

        public TrangThaiUV XemTrangThaiUV(int id)
        {
            IRepository<TrangThaiUV> repository = unitofWork.Repository<TrangThaiUV>();
            return repository.GetById(id);
        }

        public int XoaTrangThaiUV(int id)
        {
            IRepository<TrangThaiUV> repository = unitofWork.Repository<TrangThaiUV>();
            logger.Info("Xoa trang thai ung vien co ID = " + id.ToString());
            repository.Delete(repository.GetById(id));
            unitofWork.SaveChange();
            return 0;
        }
    }
}
