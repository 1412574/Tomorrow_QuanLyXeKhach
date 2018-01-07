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
    public class BangChamCongService : IBangChamCongService<BangChamCong>
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        IUnitOfWork unitofWork = new GenericUnitOfWork();

        public int CapNhatThongTinBCC(BangChamCong bangChamCong)
        {
            logger.Info("Bat dau cap nhat thong tin cham cong");
            int ret = 0;
            try
            {
                IRepository<BangChamCong> repository = unitofWork.Repository<BangChamCong>();
                repository.Update(bangChamCong);
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

        public int ThemBangChamCong(BangChamCong bangChamCong)
        {
            logger.Info("Bat dau them cham cong");
            int ret = 0;
            try
            {
                IRepository<BangChamCong> repository = unitofWork.Repository<BangChamCong>();
                repository.Add(bangChamCong);
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

        public IList<BangChamCong> XemThongTinBCC()
        {
            return unitofWork.Repository<BangChamCong>().GetAll().ToList();
        }

        public BangChamCong XemThongTinBCC(int id)
        {
            return unitofWork.Repository<BangChamCong>().GetById(id);
        }

        public IList<BangChamCong> XemThongTinBCC(string filter)
        {
            IRepository<BangChamCong> repository = unitofWork.Repository<BangChamCong>();
            return repository.GetAll(bcc => filter == null || (bcc.maCC.ToString().Contains(filter)) ||
                                                            bcc.maNV.ToString().Contains(filter) ||
                                                            bcc.ngay.ToString().Contains(filter)
                                                            ).ToList();
        }

        public int XoaBangChamCong(int id)
        {
            IRepository<BangChamCong> repository = unitofWork.Repository<BangChamCong>();
            logger.Info("Xoa cham cong co ID = " + id.ToString());
            repository.Delete(repository.GetById(id));
            unitofWork.SaveChange();
            return 0;
        }
    }
}
