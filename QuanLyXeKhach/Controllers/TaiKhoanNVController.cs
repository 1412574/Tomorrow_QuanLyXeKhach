using DataModel;
using DataService;
using NLog;
using QuanLyXeKhach.Common;
using QuanLyXeKhach.Models;
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
        //GET: Login
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Add(Common.CommonConstants.userName, null);
            Session.Add(Common.CommonVaiTro.vaiTro, null);
            return View("Login");
        }
        //POST: Login
        public ActionResult DangNhap(TaiKhoanNV taiKhoanNV)
        {
            if(ModelState.IsValid){
                string pass = taiKhoanNVService.GetMd5Hash(md5Hash, taiKhoanNV.matKhau);
                nhanVien = nhanVienService.XemNhanVienNV(taiKhoanNV.maNV);
                taiKhoan = taiKhoanNVService.XemTaiKhoanNV(taiKhoanNV.maNV);

                if (nhanVien == null)
                {
                    ModelState.AddModelError("", "Tên đăng nhập không đúng.");
                }
                else if (taiKhoanNVService.VerifyMd5Hash(md5Hash, nhanVien.cCCD, pass) && taiKhoanNVService.VerifyMd5Hash(md5Hash, taiKhoanNV.matKhau, taiKhoan.matKhau))
                {

                    //return RedirectToAction("Edit", "TaiKhoanNV", new { id = taiKhoanNV.maNV });
                    var userSession = new Common.PassWord();
                    userSession.maNV = taiKhoanNV.maNV;
                    Session.Add(Common.CommonPassword.nhanVien, userSession);
                    return RedirectToAction("Edit", "TaiKhoanNV"); 
                }
                else if (taiKhoanNVService.VerifyMd5Hash(md5Hash, taiKhoanNV.matKhau, taiKhoan.matKhau) != true)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng.");
                }
                else
                {
                    var userSession = new Common.UserLogin();
                    userSession.UserID = taiKhoanNV.maNV;
                    var vaiTroSession = new Common.VaiTroID();
                    vaiTroSession.vaiTroID = nhanVien.maVT;
                    Session.Add(Common.CommonConstants.userName, userSession);
                    Session.Add(Common.CommonVaiTro.vaiTro, vaiTroSession);
                    return RedirectToAction("Home", "PhongBan");
                }
            }
            return View("Login");
        }
        //GET:
        public ActionResult Edit()
        {
            return View();
        }
        //POST:
        public ActionResult CapNhatTaiKhoanNV(PassWords taiKhoanNV)
        {
            logger.Info("Start controller....");
            if (String.Compare(taiKhoanNV.passWord, taiKhoanNV.passWordCF, true) == 0)
            {
                var session = (PassWord)Session[CommonPassword.nhanVien];
                int maNV = session.maNV;
                TaiKhoanNV taiKhoan = new TaiKhoanNV();
                taiKhoan = taiKhoanNVService.XemTaiKhoanNV(maNV);
                taiKhoan.matKhau = taiKhoanNVService.GetMd5Hash(md5Hash, taiKhoanNV.passWord);
                int status = taiKhoanNVService.CapNhatTaiKhoan(taiKhoan);
                if (status == 0)
                {
                    logger.Info("Status: Success");
                    return RedirectToAction("Login", "TaiKhoanNV");
                }
                else
                {
                    logger.Info("Status: Fail");
                    return Content("Thất bại");
                }
            }
            else
            {
                ModelState.AddModelError("", "Xác nhận mật khẩu sai.");
            }
            return View("Edit");
        }

    }
}