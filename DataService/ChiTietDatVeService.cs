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
    public class ChiTietDatVeService : IChiTietDatVeService<ChiTietDatVe>
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        IUnitOfWork unitofWork = new GenericUnitOfWork();

        public int themChiTietDatVe(ChiTietDatVe t)
        {
            logger.Info("Start them chi tiet dat ve method");
            int ret = 0;
            try
            {
                IRepository<ChiTietDatVe> repository = unitofWork.Repository<ChiTietDatVe>();
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

        public int xoaChiTietDatVe(int id)
        {
            int ret = 0;
            try
            {
                IRepository<ChiTietDatVe> repository = unitofWork.Repository<ChiTietDatVe>();
                IList<ChiTietDatVe> list = new List<ChiTietDatVe>();
                list = repository.GetAll().ToList();
                for (int i = 0; i < list.Count(); i++)
                {
                    if (list[i].maCTDV == id)
                    {
                        repository.Delete(list[i]);
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
