using DAO;
using DataModel;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public class ThongKeService : IThongKeService<ThongKe>
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        IUnitOfWork unitofWork = new GenericUnitOfWork();
        public int CapNhatThongKe(ThongKe thongKe)
        {

            logger.Info("Start cap nhat phong ban method");
            int ret = 0;
            try
            {
                IRepository<ThongKe> repository = unitofWork.Repository<ThongKe>();
                repository.Update(thongKe);
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

        public ThongKe GetThongKe(int id)
        {
            IRepository<ThongKe> repository = unitofWork.Repository<ThongKe>();
            return repository.GetById(id);
        }

        public int ThemThongKe(ThongKe thongKe)
        {
            logger.Info("Start them thongke method");
            int ret = 0;
            try
            {
                IRepository<ThongKe> repository = unitofWork.Repository<ThongKe>();
                repository.Add(thongKe);
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

        public IList<ThongKe> XemThongKe(ThongKe thongKe)
        {
            IRepository<ThongKe> repository = unitofWork.Repository<ThongKe>();
            IList<ThongKe> listThongKe = new List<ThongKe>();
            listThongKe = repository.GetAll().ToList();
            return listThongKe;
        }

        public int XoaThongKe(int maBienBan)
        {
            int ret = 0;
            try
            {
                IRepository<ThongKe> repository = unitofWork.Repository<ThongKe>();
                IList<ThongKe> listThongKe = new List<ThongKe>();
                listThongKe = repository.GetAll().ToList();
                for (int i = 0; i < listThongKe.Count(); i++)
                {
                    if (listThongKe[i].maBienBan == maBienBan)
                    {
                        repository.Delete(listThongKe[i]);
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

