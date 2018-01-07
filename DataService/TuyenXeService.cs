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
    public class TuyenXeService : ITuyenXeService<TuyenXe>
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        IUnitOfWork unitofWork = new GenericUnitOfWork();
        public int CapNhatTuyenXe(TuyenXe tuyenXe)
        {
            logger.Info("Start cap nhat tuyen xe method");
            int ret = 0;
            try
            {
                IRepository<TuyenXe> repository = unitofWork.Repository<TuyenXe>();
                repository.Update(tuyenXe);
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

        public int ThemTuyenXe(TuyenXe tuyenXe)
        {
            logger.Info("Start them tuyen xe method");
            int ret = 0;
            try
            {
                IRepository<TuyenXe> repository = unitofWork.Repository<TuyenXe>();
                repository.Add(tuyenXe);
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

        public IList<TuyenXe> XemTuyenXe()
        {
            IRepository<TuyenXe> repository = unitofWork.Repository<TuyenXe>();
            return repository.GetAll().ToList();
        }

        public int XoaTuyenXe(int id)
        {
            IRepository<TuyenXe> repository = unitofWork.Repository<TuyenXe>();
            repository.Delete(repository.GetById(id));
            unitofWork.SaveChange();
            return 0;
        }

        public TuyenXe LayTuyenXe(int id)
        {
            IRepository<TuyenXe> repository = unitofWork.Repository<TuyenXe>();
            return repository.GetById(id);
        }
    }
}
