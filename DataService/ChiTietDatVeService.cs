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
    public class ChiTietDatVeService : IChiTietDatVeService<ChuyenXe>
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        IUnitOfWork unitofWork = new GenericUnitOfWork();
        
    }
}
