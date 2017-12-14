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
    public class DatVeController : Controller
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        IDatVeService<DatVe> _service;
        IChuyenXeService<ChuyenXe> _serviceSX;
        IKhachHangService<KhachHang> _serviceKH;
        DatVe _dv;

        public DatVeController(IDatVeService<DatVe> service, IChuyenXeService<ChuyenXe> serviceSX,
        IKhachHangService<KhachHang> serviceKH, DatVe dv)
        {
            this._service = service;
            this._serviceSX = serviceSX;
            this._serviceKH = serviceKH;
            _dv = dv;

        }
        // GET: DatVe
        public ActionResult Index()
        {
            var list = _service.xemDatVe();
            return View(list);
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult ThemDatVe(DatVe dv)
        {
            logger.Info("Start controller....");

            if(dv.ChuyenXe == null) return Content("Chuyến xe phải tồn tại");
            if(dv.KhachHang == null) return Content("Khách hàng phải tồn tại");

            int status = _service.themDatVe(dv);
            if (status == 0)
            {
                logger.Info("Status: Success");
                return RedirectToAction("index", "DatVe");
            }
            else
            {
                logger.Info("Status: Fail");
                return Content("Thêm thất bại");
            }
        }

        public ActionResult Edit(int id)
        {
            DatVe dv = _service.layDatVe(id);
            if (dv == null) return HttpNotFound();

            return View(dv);
        }
        public ActionResult CapNhatDatVe(DatVe dv)
        {
            logger.Info("Start controller....");
            int status = _service.capnhatDatVe(dv);
            if (status == 0)
            {
                logger.Info("Status: Success");
                return RedirectToAction("index", "DatVe");
            }
            else
            {
                logger.Info("Status: Fail");
                return Content("Cập nhật thất bại");
            }
        }

        public ActionResult Delete(int id)
        {
            DatVe dv = _service.layDatVe(id);
            if (dv == null) return HttpNotFound();

            return View(dv);
        }

        public ActionResult XoaDatVe(DatVe dv)
        {
            logger.Info("Start controller....");
            int status = _service.xoaDatVe(dv.maDatVe);
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
    }
}