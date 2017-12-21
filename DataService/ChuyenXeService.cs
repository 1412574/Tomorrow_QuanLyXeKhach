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
    public class ChuyenXeService : IChuyenXeService<ChuyenXe>
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        IUnitOfWork unitofWork = new GenericUnitOfWork();
        public int CapNhatChuyenXe(ChuyenXe chuyenXe)
        {
            logger.Info("Start cap nhat chuyen xe method");
            int ret = 0;
            try
            {
                IRepository<ChuyenXe> repository = unitofWork.Repository<ChuyenXe>();
                repository.Update(chuyenXe);
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



        public int ThemChuyenXe(ChuyenXe chuyenXe)
        {
            logger.Info("Start them chuyen xe method");
            int ret = 0;
            try
            {
                IRepository<ChuyenXe> repository = unitofWork.Repository<ChuyenXe>();
                repository.Add(chuyenXe);
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

        public IList<ChuyenXe> XemChuyenXe()
        {
            IRepository<ChuyenXe> repository = unitofWork.Repository<ChuyenXe>();
            return repository.GetAll().ToList();
        }

        public int XoaChuyenXe(int id)
        {
            IRepository<ChuyenXe> repository = unitofWork.Repository<ChuyenXe>();
            repository.Delete(repository.GetById(id));
            unitofWork.SaveChange();
            return 0;
        }

        public ChuyenXe LayChuyenXe (int id)
        {
            IRepository<ChuyenXe> repository = unitofWork.Repository<ChuyenXe>();
            return repository.GetById(id);
        }

        public List<int> danhSachGheTrong(int id)
        {
            List<int> ghe = Enumerable.Range(1, 50).ToList<int>();
            ChuyenXe chuyenXe = LayChuyenXe(id);
            ICollection<DatVe> datVes = chuyenXe.DatVes;
            foreach (var datVe in datVes)
            {
                ICollection<ChiTietDatVe> ctdvs = datVe.ChiTietDatVes;
                foreach(var ctdv in ctdvs) {
                    if(ghe.Contains(ctdv.soGhe))
                    {
                        ghe.Remove(ctdv.soGhe);
                    }
                }
            }
            return ghe;
        }

    }
}
