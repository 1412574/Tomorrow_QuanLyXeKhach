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
    public class TramXeService : ITramXeService<TramXe>
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        IUnitOfWork unitofWork = new GenericUnitOfWork();
        public int CapNhatTramXe(TramXe tramXe)
        {
            logger.Info("Start cap nhat tram xe method");
            int ret = 0;
            try
            {
                IRepository<TramXe> repository = unitofWork.Repository<TramXe>();
                repository.Update(tramXe);
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

        public int ThemTramXe(TramXe tramXe)
        {
            logger.Info("Start them tram xe method");
            int ret = 0;
            try
            {
                IRepository<TramXe> repository = unitofWork.Repository<TramXe>();
                repository.Add(tramXe);
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

        public IList<TramXe> XemTramXe()
        {
            IRepository<TramXe> repository = unitofWork.Repository<TramXe>();
            return repository.GetAll().ToList();
        }

        public int XoaTramXe(int id)
        {
            IRepository<TramXe> repository = unitofWork.Repository<TramXe>();
            repository.Delete(repository.GetById(id));
            unitofWork.SaveChange();
            return 0;
        }

        public TramXe LayTramXe(int id)
        {
            IRepository<TramXe> repository = unitofWork.Repository<TramXe>();
            return repository.GetById(id);
        }

        public IList<TramXe> XemHanhTrinhTheoMaTuyen(int id)
        {
            IRepository<HanhTrinh> repositoryHT = unitofWork.Repository<HanhTrinh>();
            IList<HanhTrinh> ht = repositoryHT.GetAll(h => h.MaTuyenXe == id).ToList();
            IRepository<TramXe> repositoryTX = unitofWork.Repository<TramXe>();
            IList<TramXe> tx = new List<TramXe>();
            foreach (var item in ht)
            {
                tx.Add(repositoryTX.GetById(item.MaTramXe));
            }
            return tx;
        }
    }
}
