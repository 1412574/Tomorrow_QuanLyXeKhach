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
    public class KhachHangController : Controller
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        IKhachHangService<KhachHang> _serviceKH;
        KhachHang _kh;

        public KhachHangController(IKhachHangService<KhachHang> service, KhachHang kh)
        {
            this._serviceKH = service;
            _kh = kh;

        }

        // GET: KhachHang
        public ActionResult Index()
        {
            var listkh = _serviceKH.xemKhachHang();
            return View(listkh);
        }


        public ActionResult Them()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            KhachHang kh = _serviceKH.layKhachHang(id);
            if (kh == null) return HttpNotFound();

            return View(kh);
        }
        public ActionResult Delete(int id)
        {
            KhachHang kh = _serviceKH.layKhachHang(id);
            if (kh == null) return HttpNotFound();

            return View(kh);
        }

        public ActionResult XoaKhachHang(KhachHang kh)
        {
            logger.Info("Start controller....");
            int status = _serviceKH.xoaKhachHang(kh.maKhachHang);
            if (status == 0)
            {
                logger.Info("Status: Success");
                return RedirectToAction("index", "KhachHang");
            }
            else
            {
                logger.Info("Status: Fail");
                return Content("Xóa thất bại");
            }
        }
        public ActionResult Huyxoakhachhang()
        {
            return RedirectToAction("index", "KhachHang");
        }
        public ActionResult CapNhatKhachHang(KhachHang kh)
        {
            logger.Info("Start controller....");
            int status = _serviceKH.capnhatKhachHang(kh);
            if (status == 0)
            {
                logger.Info("Status: Success");
                return RedirectToAction("index", "KhachHang");
            }
            else
            {
                logger.Info("Status: Fail");
                return Content("Cập nhật thất bại");
            }
        }

        public ActionResult AddCustomer(KhachHang kh)
        {
            logger.Info("Start controller....");
            int status = _serviceKH.themKhachHang(kh);
            if (status == 0)
            {
                logger.Info("Status: Success");
                return RedirectToAction("index", "KhachHang");
            }
            else
            {
                logger.Info("Status: Fail");
                return Content("Thêm thất bại");
            }
        }
    }
}