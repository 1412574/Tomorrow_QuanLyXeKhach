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
            throw new NotImplementedException();
        }

        public int ThemTuyenXe(TuyenXe tuyenXe)
        {
            logger.Info("Start them chuyen xe method");
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

        public IEnumerable<TuyenXe> XemTuyenXe()
        {
            IRepository<TuyenXe> repository = unitofWork.Repository<TuyenXe>();
            return repository.GetAll();
        }

        public int XoaTuyenXe(int id)
        {
            throw new NotImplementedException();
        }
    }
}
