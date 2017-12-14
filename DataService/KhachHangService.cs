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
    public class KhachHangService : IKhachHangService<KhachHang>
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        IUnitOfWork unitofWork = new GenericUnitOfWork();

        public int themKhachHang(KhachHang t)
        {
            logger.Info("Start them khach hang method");
            int ret = 0;
            try
            {
                IRepository<KhachHang> repository = unitofWork.Repository<KhachHang>();
                repository.Add(t);
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
        public IList<KhachHang> xemKhachHang()
        {
            IRepository<KhachHang> repository = unitofWork.Repository<KhachHang>();
            IList<KhachHang> lisKH = new List<KhachHang>();
            lisKH = repository.GetAll().ToList();
            return lisKH;
        }

        public KhachHang layKhachHang(int id)
        {
            IRepository<KhachHang> repository = unitofWork.Repository<KhachHang>();
            return repository.GetById(id);
        }
        public int xoaKhachHang(int id)
        {
            int ret = 0;
            try
            {
                IRepository<KhachHang> repository = unitofWork.Repository<KhachHang>();
                IList<KhachHang> listkh = new List<KhachHang>();
                listkh = repository.GetAll().ToList();
                for (int i = 0; i < listkh.Count(); i++)
                {
                    if (listkh[i].maKhachHang == id)
                    {
                        repository.Delete(listkh[i]);
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

        public int capnhatKhachHang(KhachHang t)
        {
            logger.Info("Start cap nhat phong ban method");
            int ret = 0;
            try
            {
                IRepository<KhachHang> repository = unitofWork.Repository<KhachHang>();
                repository.Update(t);
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
    }
}
