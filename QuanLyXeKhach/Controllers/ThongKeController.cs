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
    public class ThongKeController : Controller
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        IThongKeService<ThongKe> service;
        ThongKe _thongKe;

        public ThongKeController(IThongKeService<ThongKe> service, ThongKe thong)
        {
            this.service = service;
            _thongKe = thong;
        
        }
        // GET: ThongKe
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ThemThongKe(ThongKe thongKe)
        {
            logger.Info("Start controller....");
            int status = service.ThemThongKe(thongKe);
            if (status == 0)
            {
                logger.Info("Status: Success");
                return RedirectToAction("Menu", "ThongKe");
            }
            else
            {
                logger.Info("Status: Fail");
                return Content("Thêm thất bại");
            }
        }
        //GET:
        public ActionResult Menu(ThongKe thongKe)
        {
            var listThongKe = service.XemThongKe(thongKe);
            return View(listThongKe);
        }
        //POST:
        public ActionResult XoaThongKe(int id)
        {

            logger.Info("Start controller....");
            int status = service.XoaThongKe(id);
            if (status == 0)
            {
                logger.Info("Status: Success");
                return RedirectToAction("Menu", "ThongKe");
            }
            else
            {
                logger.Info("Status: Fail");
                return Content("Xóa thất bại");
            }
        }
        //POST:
        public ActionResult Edit(int id)
        {

            var modelView = service.GetThongKe(id);
            return View(modelView);
        }
        public ActionResult CapNhatThongKe(ThongKe thongKe)
        {
            logger.Info("Start controller....");
            int status = service.CapNhatThongKe(thongKe);
            if (status == 0)
            {
                logger.Info("Status: Success");
                return RedirectToAction("Menu", "ThongKe");
            }
            else
            {
                logger.Info("Status: Fail");
                return Content("Cập nhật thất bại");
            }
        }
        //GET:
        public ActionResult Details(int id)
        {
            var modelView = service.GetThongKe(id);
            return View(modelView);
        }

    }
}
