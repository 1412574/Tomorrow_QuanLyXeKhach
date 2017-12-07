using DataModel;
using DataService;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyXeKhach.Controllers
{
    public class PhongBanController : Controller
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        IPhongBanService<PhongBan> service;

        public PhongBanController(IPhongBanService<PhongBan> service)
        {
            this.service = service;
        }
        // GET: PhongBan
        public ActionResult Index()
        {
            PhongBan phongBan = new PhongBan();
            return View(phongBan);
        }
        public ActionResult ThemPhongBan(PhongBan phongBan)
        {
            logger.Info("Start controller....");
            int status = service.ThemPhongBan(phongBan);
            if (status == 0)
            {
                logger.Info("Status: Success");
                return Content("Them thanh cong");
            }
            else
            {
                logger.Info("Status: Fail");
                return Content("Them that bai");
            }
        }
    }
}