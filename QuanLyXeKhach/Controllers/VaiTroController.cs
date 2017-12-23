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
    public class VaiTroController : Controller
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        IVaiTroService<VaiTro> vaiTroService;

        public VaiTroController(IVaiTroService<VaiTro> vaiTroService)
        {
            this.vaiTroService = vaiTroService;
        }
        // GET: VaiTro
        public ActionResult Index()
        {
            return View();
        }
        //Get: NhanVien
        public ActionResult ThemVaiTro()
        {
            return View();
        }
        //POST: VaiTro
        public ActionResult ThemVaiTroNV(VaiTro vaiTro)
        {
            logger.Info("Start controller....");
            int status = vaiTroService.ThemVaiTro(vaiTro);
            if (status == 0)
            {
                logger.Info("Status: Success");
                return RedirectToAction("XemVaiTro", "VaiTro");
            }
            else
            {
                logger.Info("Status: Fail");
                return Content("Thêm thất bại");
            }

        }
        //GET: Lấy danh sách vai trò
        public ActionResult XemVaiTro()
        {
            var listVT = vaiTroService.XemVaiTro();
            return View(listVT);
        }
        //GET: Lấy thông tin chi tiết vai trò
        public ActionResult XemVaiTroNV(int id)
        {
            var modelView = vaiTroService.XemVaiTroNV(id);
            return View(modelView);
        }
        //
        public ActionResult Edit(int id)
        {
            var modelView = vaiTroService.XemVaiTroNV(id);
            return View(modelView);
        }
        //POST: Lấy thông tin cập nhật từ giao diện 
        public ActionResult CapNhatVaiTro(VaiTro vaiTro)
        {
            logger.Info("Start controller....");
            int status = vaiTroService.CapNhatVaiTro(vaiTro);
            if (status == 0)
            {
                logger.Info("Status: Success");
                return RedirectToAction("XemVaiTro", "VaiTro");
            }
            else
            {
                logger.Info("Status: Fail");
                return Content("Cập nhật thất bại");
            }
        }
        //GET: Lấy thông tin vai trò theo mã
        public ActionResult XacNhanXoa(int id)
        {
            var modelView = vaiTroService.XemVaiTroNV(id);
            return View(modelView);
        }
        //POST: Xác nhận xóa vai trò.
        public ActionResult XoaVaiTro(VaiTro vaiTro)
        {
            logger.Info("Start controller....");
            int status = vaiTroService.XoaVaiTro(vaiTro.maVT);
            if (status == 0)
            {
                logger.Info("Status: Success");
                return RedirectToAction("XemVaiTro", "VaiTro");
            }
            else
            {
                logger.Info("Status: Fail");
                return Content("Xóa thất bại");
            }
        }
    }
}