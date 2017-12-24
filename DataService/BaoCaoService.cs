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
    public class BaoCaoService : IBaoCaoService<BaoCao>
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        IUnitOfWork unitofWork = new GenericUnitOfWork();
        public int CapNhatBaoCao (BaoCao baoCao)
        {

            logger.Info("Start cap nhat phong ban method");
            int ret = 0;
            try
            {
                IRepository<BaoCao> repository = unitofWork.Repository<BaoCao>();
                repository.Update(baoCao);
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

        public BaoCao GetBaoCao(int id)
        {
            IRepository<BaoCao> repository = unitofWork.Repository<BaoCao>();
            return repository.GetById(id);
        }

        public int ThemBaoCao(BaoCao baoCao)
        {
            logger.Info("Start them bao cao method");
            int ret = 0;
            try
            {
                IRepository<BaoCao> repository = unitofWork.Repository<BaoCao>();
                repository.Add(baoCao);
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

        public IList<BaoCao> XemBaoCao(BaoCao baoCao)
        {
            IRepository<BaoCao> repository = unitofWork.Repository<BaoCao>();
            IList<BaoCao> listBaoCao = new List<BaoCao>();
            listBaoCao = repository.GetAll().ToList();
            return listBaoCao;
        }

        public int XoaBaoCao(int maBienBan)
        {
            //int ret = 0;
            //try
            //{
            //    IRepository<BaoCao> repository = unitofWork.Repository<BaoCao>();
            //    IList<BaoCao> listBaoCao = new List<BaoCao>();
            //    listBaoCao = repository.GetAll().ToList();
            //    for (int i = 0; i < listBaoCao.Count(); i++)
            //    {
            //        if (listBaoCao[i].maBienBan == maBienBan)
            //        {
            //            repository.Delete(listBaoCao[i]);
            //            unitofWork.SaveChange();
            //            logger.Info("Status: Success");
            //        }
            //    }
            //}
            //catch
            //{
            //    logger.Info("Status: Fail");
            //    ret = -1;
            //}
            //return ret;I
            IRepository<BaoCao> repository = unitofWork.Repository<BaoCao>();
            repository.Delete(repository.GetById(maBienBan));
            unitofWork.SaveChange();
            return 0;
        }
    }
}

