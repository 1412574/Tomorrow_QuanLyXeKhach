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
    public class VaiTroService:IVaiTroService<VaiTro>
    {

        ILogger logger = LogManager.GetCurrentClassLogger();
        IUnitOfWork unitofWork = new GenericUnitOfWork();
        //cập nhật vai trò của nhân viên
        public int CapNhatVaiTro(VaiTro vaiTro)
        {
            logger.Info("Start cap nhat vai tro method");
            int ret = 0;
            try
            {
                IRepository<VaiTro> repository = unitofWork.Repository<VaiTro>();
                repository.Update(vaiTro);
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

        public int ThemVaiTro(VaiTro vaiTro)
        {
            logger.Info("Start them phong ban method");
            int ret = 0;
            try
            {
                IRepository<VaiTro> repository = unitofWork.Repository<VaiTro>();
                repository.Add(vaiTro);
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

        public IList<VaiTro> XemVaiTro()
        {
            IRepository<VaiTro> repository = unitofWork.Repository<VaiTro>();
            IList<VaiTro> listVaiTro = new List<VaiTro>();
            listVaiTro = repository.GetAll().ToList();
            return listVaiTro;
        }

        public VaiTro XemVaiTroNV(int id)
        {
            IRepository<VaiTro> repository = unitofWork.Repository<VaiTro>();
            return repository.GetById(id);
        }
        //Xóa vai trò theo mã vai trò
        public int XoaVaiTro(int maVT)
        {
            int ret = 0;
            try
            {
                IRepository<VaiTro> repository = unitofWork.Repository<VaiTro>();
                IList<VaiTro> listVaiTro = new List<VaiTro>();
                listVaiTro = repository.GetAll().ToList();
                for (int i = 0; i < listVaiTro.Count(); i++)
                {
                    if (listVaiTro[i].maVT == maVT)
                    {
                        repository.Delete(listVaiTro[i]);
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
