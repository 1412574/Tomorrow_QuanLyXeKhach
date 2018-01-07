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
    public class HanhTrinhService : IHanhTrinhService<HanhTrinh>
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        IUnitOfWork unitofWork = new GenericUnitOfWork();
        public int CapNhatHanhTrinh(HanhTrinh hanhTrinh)
        {
            logger.Info("Start cap nhat hanh trinh method");
            int ret = 0;
            try
            {
                IRepository<HanhTrinh> repository = unitofWork.Repository<HanhTrinh>();
                repository.Update(hanhTrinh);
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

        public int ThemHanhTrinh(HanhTrinh hanhTrinh)
        {
            logger.Info("Start them hanh trinh method");
            int ret = 0;
            try
            {
                IRepository<HanhTrinh> repository = unitofWork.Repository<HanhTrinh>();
                repository.Add(hanhTrinh);
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

        public IList<HanhTrinh> XemHanhTrinh()
        {
            IRepository<HanhTrinh> repository = unitofWork.Repository<HanhTrinh>();
            return repository.GetAll().ToList();
        }

        

        public int XoaHanhTrinh(int id)
        {
            IRepository<HanhTrinh> repository = unitofWork.Repository<HanhTrinh>();
            repository.Delete(repository.GetById(id));
            unitofWork.SaveChange();
            return 0;
        }

        public HanhTrinh LayHanhTrinh(int id)
        {
            IRepository<HanhTrinh> repository = unitofWork.Repository<HanhTrinh>();
            return repository.GetById(id);
        }

        
    }
}
