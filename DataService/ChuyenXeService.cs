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
    public class ChuyenXeService : IChuyenXeService<ChuyenXe>
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        IUnitOfWork unitofWork = new GenericUnitOfWork();
        public int CapNhatChuyenXe(ChuyenXe chuyenXe)
        {
            logger.Info("Start cap nhat chuyen xe method");
            int ret = 0;
            try
            {
                IRepository<ChuyenXe> repository = unitofWork.Repository<ChuyenXe>();
                repository.Update(chuyenXe);
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

        public int ThemChuyenXe(ChuyenXe chuyenXe)
        {
            logger.Info("Start them chuyen xe method");
            int ret = 0;
            try
            {
                IRepository<ChuyenXe> repository = unitofWork.Repository<ChuyenXe>();
                repository.Add(chuyenXe);
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

        public IList<ChuyenXe> XemChuyenXe()
        {
            IRepository<ChuyenXe> repository = unitofWork.Repository<ChuyenXe>();
            return repository.GetAll().ToList();
        }

        public int XoaChuyenXe(int id)
        {
            IRepository<ChuyenXe> repository = unitofWork.Repository<ChuyenXe>();
            repository.Delete(repository.GetById(id));
            unitofWork.SaveChange();
            return 0;
        }

        public ChuyenXe LayChuyenXe (int id)
        {
            IRepository<ChuyenXe> repository = unitofWork.Repository<ChuyenXe>();
            return repository.GetById(id);
        }
    }
}
