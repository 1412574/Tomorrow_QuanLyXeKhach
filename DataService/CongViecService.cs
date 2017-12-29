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
    public class CongViecService : ICongViecService<CongViec>
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        IUnitOfWork unitofWork = new GenericUnitOfWork();

        public int CapNhatThongTinCV(CongViec congViec)
        {
            logger.Info("Bat dau cap nhat thong tin cong viec");
            int ret = 0;
            try
            {
                IRepository<CongViec> repository = unitofWork.Repository<CongViec>();
                repository.Update(congViec);
                unitofWork.SaveChange();
                logger.Info("Status: Success");
                ret = 0;
            }
            catch
            {
                logger.Info("Status: Fail");
                ret = -1;
            }
            return ret;
        }

        public int ThemCongViec(CongViec congViec)
        {
            logger.Info("Bat dau them cong viec");
            int ret = 0;
            try
            {
                IRepository<CongViec> repository = unitofWork.Repository<CongViec>();
                repository.Add(congViec);
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

        public IList<CongViec> XemThongTinCV()
        {
            IRepository<CongViec> repository = unitofWork.Repository<CongViec>();
            return repository.GetAll().ToList();
        }

        public CongViec XemThongTinCV(int id)
        {
            IRepository<CongViec> repository = unitofWork.Repository<CongViec>();
            return repository.GetById(id);
        }

        public IList<CongViec> XemThongTinCV(string filter)
        {
            IRepository<CongViec> repository = unitofWork.Repository<CongViec>();
            return repository.GetAll(cv => filter == null || (cv.maCV.ToString().Contains(filter)) ||
                                                            cv.tenCV.Contains(filter) ||
                                                            cv.hanHoanThanh.ToShortDateString().Contains(filter) 
                                                            ).ToList();
        }

        public int XoaCongViec(int id)
        {
            IRepository<CongViec> repository = unitofWork.Repository<CongViec>();
            logger.Info("Xoa cong viec co ID = " + id.ToString());
            repository.Delete(repository.GetById(id));
            unitofWork.SaveChange();
            return 0;
        }
    }
}
