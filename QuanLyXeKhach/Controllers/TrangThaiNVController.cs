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
    public class TrangThaiNVController : Controller
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        ITrangThaiNVService<TrangThaiNV> trangThaiNVService;
        public TrangThaiNVController(ITrangThaiNVService<TrangThaiNV> trangThaiNVService)
        {
            this.trangThaiNVService = trangThaiNVService;
        }
        // GET: TrangThaiNV
        public ActionResult ThemTrangThai()
        {
            return View();
        }
        //GET: Lấy toàn bộ danh sách trạng thái đưa lên giao diện
        public ActionResult XemTrangThai()
        {
            var listTT = trangThaiNVService.XemTrangThai();
            return View(listTT);
        }
        //POST: Thêm Trạng Thái nhân viên
        public ActionResult ThemTrangThaiNV(TrangThaiNV trangThaiNV)
        {
            logger.Info("Start controller....");
            int status = trangThaiNVService.ThemTrangThaiNV(trangThaiNV);
            if (status == 0)
            {
                logger.Info("Status: Success");
                return RedirectToAction("XemTrangThai", "TrangThaiNV");
            }
            else
            {
                logger.Info("Status: Fail");
                return Content("Thêm thất bại");
            }

        }
        //GET: Lấy thông tin chi tiết trạng thái
        public ActionResult XemTrangThaiNV(int id)
        {
            var modelView = trangThaiNVService.XemTrangThaiNV(id);
            return View(modelView);
        }
        //
        public ActionResult Edit(int id)
        {
            var modelView = trangThaiNVService.XemTrangThaiNV(id);
            return View(modelView);
        }
        //POST: Lấy thông tin cập nhật từ giao diện 
        public ActionResult CapNhatTrangThaiNV(TrangThaiNV trangThaiNV)
        {
            logger.Info("Start controller....");
            int status = trangThaiNVService.CapNhatTrangThaiNV(trangThaiNV);
            if (status == 0)
            {
                logger.Info("Status: Success");
                return RedirectToAction("XemTrangThai", "TrangThaiNV");
            }
            else
            {
                logger.Info("Status: Fail");
                return Content("Cập nhật thất bại");
            }
        }
        //GET: Lấy thông tin trạng thái theo mã
        public ActionResult XacNhanXoa(int id)
        {
            var modelView = trangThaiNVService.XemTrangThaiNV(id);
            return View(modelView);
        }
        //POST: Xác nhận xóa một trạng thái.
        [HttpPost, ActionName("XacNhanXoa")]
        [ValidateAntiForgeryToken]
        public ActionResult XoaTrangThaiNV(int id)
        {
            logger.Info("Start controller....");
            int status = trangThaiNVService.XoaTrangThaiNV(id);
            if (status == 0)
            {
                logger.Info("Status: Success");
                return RedirectToAction("XemTrangThai", "TrangThaiNV");
            }
            else
            {
                logger.Info("Status: Fail");
                return Content("Xóa thất bại");
            }
        }

    }
}