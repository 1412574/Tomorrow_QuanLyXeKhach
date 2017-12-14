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
    public class DatVeService : IDatVeService<DatVe>
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        IUnitOfWork unitofWork = new GenericUnitOfWork();

        public IList<DatVe> xemDatVe()
        {
            IRepository<DatVe> repository = unitofWork.Repository<DatVe>();
            IList<DatVe> list = new List<DatVe>();
            list = repository.GetAll().ToList();
            return list;
        }

        public DatVe layDatVe(int id)
        {
            IRepository<DatVe> repository = unitofWork.Repository<DatVe>();
            return repository.GetById(id);
        }

        public int themDatVe(DatVe t)
        {
            logger.Info("Start them khach hang method");
            int ret = 0;
            try
            {
                IRepository<DatVe> repository = unitofWork.Repository<DatVe>();
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

        public int xoaDatVe(int id)
        {
            int ret = 0;
            try
            {
                IRepository<DatVe> repository = unitofWork.Repository<DatVe>();
                IList<DatVe> list = new List<DatVe>();
                list = repository.GetAll().ToList();
                for (int i = 0; i < list.Count(); i++)
                {
                    if (list[i].maKhachHang == id)
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

        public int capnhatDatVe(DatVe t)
        {
            logger.Info("Start cap nhat phong ban method");
            int ret = 0;
            try
            {
                IRepository<DatVe> repository = unitofWork.Repository<DatVe>();
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
