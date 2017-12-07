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
        public int ThemPhongBan(PhongBan  phongBan)
        {
            logger.Info("Start them phong method");
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
    }
}
