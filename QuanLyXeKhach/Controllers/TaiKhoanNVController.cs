using DataModel;
using DataService;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace QuanLyXeKhach.Controllers
{
    public class TaiKhoanNVController : Controller
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        ITaiKhoanNVService<TaiKhoanNV> taiKhoanNVService;
        INhanVienService<NhanVien> nhanVienService;
        NhanVien nhanVien;
        TaiKhoanNV taiKhoan;
        MD5 md5Hash;
        public TaiKhoanNVController(ITaiKhoanNVService<TaiKhoanNV> taiKhoanNVService, INhanVienService<NhanVien> nhanVienService)
        {
            this.taiKhoanNVService = taiKhoanNVService;
            this.nhanVienService = nhanVienService;
            nhanVien = new NhanVien();
            taiKhoan = new TaiKhoanNV();
            md5Hash = MD5.Create();
        }
        // GET: TaiKhoanNV
        public ActionResult Index()
        {
            return View();
        }
        //POST: Login
        public ActionResult Login(TaiKhoanNV taiKhoanNV)
        {
            if(ModelState.IsValid){
                string pass = taiKhoanNVService.GetMd5Hash(md5Hash, taiKhoanNV.matKhau);
                nhanVien = nhanVienService.XemNhanVienNV(taiKhoanNV.maNV);
                taiKhoan = taiKhoanNVService.XemTaiKhoanNV(taiKhoanNV.maNV);

                if (nhanVien == null)
                {
                    ModelState.AddModelError("", "Tên đăng nhập không đúng.");
                }
                else if (taiKhoanNVService.VerifyMd5Hash(md5Hash, nhanVien.cCCD, pass))
                {
           
                    return RedirectToAction("Edit", "TaiKhoanNV", new { id = taiKhoanNV.maNV });
                }
                else if (taiKhoanNVService.VerifyMd5Hash(md5Hash, taiKhoanNV.matKhau, taiKhoan.matKhau) != true)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng.");
                }
                else
                {
                    return RedirectToAction("XemNhanVien", "NhanVien");
                }
            }
            return View("Index");
        }
        //GET:
        public ActionResult Edit(int id)
        {
            int a = id;
            var modelView = taiKhoanNVService.XemTaiKhoanNV(id);
            return View(modelView);
        }
        //POST:
        public ActionResult CapNhatTaiKhoanNV(TaiKhoanNV taiKhoanNV)
        {
            logger.Info("Start controller....");
            taiKhoanNV.matKhau = taiKhoanNVService.GetMd5Hash(md5Hash, taiKhoanNV.matKhau);
            int status = taiKhoanNVService.CapNhatTaiKhoan(taiKhoanNV);
            if (status == 0)
            {
                logger.Info("Status: Success");
                return RedirectToAction("Index", "TaiKhoanNV");
            }
            else
            {
                logger.Info("Status: Fail");
                return Content("Thất bại");
            }
        }
        //

    }
}