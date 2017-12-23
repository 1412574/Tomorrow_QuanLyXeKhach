using DAO;
using DataModel;
using NLog;
using System.Collections.Generic;
using System.Linq;

namespace DataService
{
    public class UngVienService : IUngVienService<UngVien>
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        IUnitOfWork unitofWork = new GenericUnitOfWork();
        public int CapNhatThongTinUV(UngVien ungVien)
        {
            logger.Info("Bat dau cap nhat thong tin ung vien");
            int ret = 0;
            try
            {
                IRepository<UngVien> repository = unitofWork.Repository<UngVien>();
                repository.Update(ungVien);
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

        public int ThemUngVien(UngVien ungVien)
        {
            logger.Info("Bat dau them ung vien");
            int ret = 0;
            try
            {
                IRepository<UngVien> repository = unitofWork.Repository<UngVien>();
                repository.Add(ungVien);
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

        public IList<UngVien> XemThongTinUV()
        {
            IRepository<UngVien> repository = unitofWork.Repository<UngVien>();
            return repository.GetAll().ToList();
        }

        public UngVien XemThongTinUV(int id)
        {
            IRepository<UngVien> repository = unitofWork.Repository<UngVien>();
            return repository.GetById(id);
        }

        public int XoaUngVien(int id)
        {
            IRepository<UngVien> repository = unitofWork.Repository<UngVien>();
            logger.Info("Xoa ung vien co ID = " + id.ToString());
            repository.Delete(repository.GetById(id));
            unitofWork.SaveChange();
            return 0;
        }
    }
}
