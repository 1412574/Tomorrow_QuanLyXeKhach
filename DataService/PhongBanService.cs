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
    public class PhongBanService : IPhongBanService<PhongBan>
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        IUnitOfWork unitofWork = new GenericUnitOfWork();

        public int CapNhatPhongBan(PhongBan phongBan)
        {
            logger.Info("Start cap nhat phong ban method");
            int ret = 0;
            try
            {
                IRepository<PhongBan> repository = unitofWork.Repository<PhongBan>();
                repository.Update(phongBan);
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

        public PhongBan GetPhongBan(int id)
        {
            IRepository<PhongBan> repository = unitofWork.Repository<PhongBan>();
            return repository.GetById(id);
        }

        public int ThemPhongBan(PhongBan  phongBan)
        {
            logger.Info("Start them phong ban method");
            int ret = 0;
            try
            {
                IRepository<PhongBan> repository = unitofWork.Repository<PhongBan>(); 
                repository.Add(phongBan);
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

        public IList<PhongBan> XemPhongBan(PhongBan phongBan)
        {
            
            IRepository<PhongBan> repository = unitofWork.Repository<PhongBan>();
            IList<PhongBan> listPhongBan = new List<PhongBan>();
            listPhongBan = repository.GetAll().ToList();
            return listPhongBan;
        }

        public int XoaPhongBan(int maPB)
        {
            int ret = 0;
            try
            {
                IRepository<PhongBan> repository = unitofWork.Repository<PhongBan>();
                IList<PhongBan> listPhongBan = new List<PhongBan>();
                listPhongBan = repository.GetAll().ToList();
                for(int i = 0; i< listPhongBan.Count(); i++)
                {
                   if( listPhongBan[i].maPB == maPB)
                    {
                        repository.Delete(listPhongBan[i]);
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
