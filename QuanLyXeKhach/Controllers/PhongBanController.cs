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
        PhongBan _phongBan;

        public PhongBanController(IPhongBanService<PhongBan> service, PhongBan phong)
        {
            this.service = service;
            _phongBan = phong;
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
        //
        public ActionResult Menu(PhongBan phongBan)
        {
            var listPhong = service.XemPhongBan(phongBan);
            return View(listPhong);
        }
        //
        public ActionResult XoaPhongBan(int id)
        {
            
            logger.Info("Start controller....");
            int status = service.XoaPhongBan(id);
            if (status == 0)
            {
                logger.Info("Status: Success");
                return Content("Xóa thành công");
            }
            else
            {
                logger.Info("Status: Fail");
                return Content("Xóa thất bại");
            }
            //return View();
        }
        //
        public ActionResult Edit(int id)
        {

            var modelView = service.GetPhongBan(id);
            return View(modelView);
        }
        public ActionResult CapNhatPhongBan(PhongBan phongBan)
        {
            logger.Info("Start controller....");
            int status = service.CapNhatPhongBan(phongBan);
            if (status == 0)
            {
                logger.Info("Status: Success");
                return Content("Cập nhật thành công");
            }
            else
            {
                logger.Info("Status: Fail");
                return Content("Cập nhật thất bại");
            }
        }

    }
}