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
    public class NhanVienController : Controller
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        INhanVienService<NhanVien> nhanVienService;
        ITrangThaiNVService<TrangThaiNV> trangThaiNVService;
        IVaiTroService<VaiTro> vaiTroService;
        ITaiKhoanNVService<TaiKhoanNV> taiKhoanNVService;
        IPhongBanService<PhongBan> phongBanService;
        MD5 md5Hash;

        public NhanVienController(INhanVienService<NhanVien> nhanVienService, ITrangThaiNVService<TrangThaiNV> trangThaiNVService, IVaiTroService<VaiTro> vaiTroService, ITaiKhoanNVService<TaiKhoanNV> taiKhoanNVService, IPhongBanService<PhongBan> phongBanService)
        {
            this.nhanVienService = nhanVienService;
            this.vaiTroService = vaiTroService;
            this.trangThaiNVService = trangThaiNVService;
            this.taiKhoanNVService = taiKhoanNVService;
            this.phongBanService = phongBanService;
            md5Hash = MD5.Create();
        }
        // GET: NhanVien
        public ActionResult XemNhanVien()
        {
            IList < NhanVien > listNV = new List<NhanVien>();
            listNV = nhanVienService.XemNhanVien().ToList();
            return View(listNV);
        }
        //View thêm mới nhân viên
        public ActionResult Create()
        {
            ViewBag.maVT = new SelectList(vaiTroService.XemVaiTro(), "maVT", "tenVT");
            ViewBag.maTT = new SelectList(trangThaiNVService.XemTrangThai(), "maTT", "tenTT");
            ViewBag.maPB = new SelectList(phongBanService.XemPhongBan(), "maPB", "tenPB");
            return View();
        }
        //POST: Lấy thông tin nhân viên từ giao diện
        public ActionResult ThemNhanVien(NhanVien nhanVien)
        {
            TaiKhoanNV taiKhoanNV = new TaiKhoanNV();
            logger.Info("Start controller....");
            int status = nhanVienService.ThemNhanVien(nhanVien);
            IList <NhanVien> listNV = new List<NhanVien>();
            listNV = nhanVienService.XemNhanVien().ToList();
            for(int i =0 ; i< listNV.Count; i++)
            {
                if (listNV[i].cCCD == nhanVien.cCCD)
                {
                    taiKhoanNV.maNV = listNV[i].maNV;
                    break;
                }
            }
            string str = taiKhoanNVService.GetMd5Hash(md5Hash, nhanVien.cCCD);
            taiKhoanNV.matKhau = str;
            int statusTK = taiKhoanNVService.ThemTaiKhoan(taiKhoanNV);
            if (status == 0 && statusTK == 0)
            {
                logger.Info("Status: Success");
                return RedirectToAction("XemNhanVien", "NhanVien");
            }
            else
            {
                logger.Info("Status: Fail");
                return Content("Thêm thất bại");
            }
        }
        //GET: 
        public ActionResult XemNhanVienNV(int id)
        {
            return View(nhanVienService.XemNhanVienNV(id));
        }
        //GET:
        public ActionResult Edit(int id)
        {
            ViewBag.maVT = new SelectList(vaiTroService.XemVaiTro(), "maVT", "tenVT");
            ViewBag.maTT = new SelectList(trangThaiNVService.XemTrangThai(), "maTT", "tenTT");
            ViewBag.maPB = new SelectList(phongBanService.XemPhongBan(), "maPB", "tenPB");
            return View(nhanVienService.XemNhanVienNV(id));
        }
        //POST:
        public ActionResult CapNhatNhanVien(NhanVien nhanVien)
        {
            logger.Info("Start controller....");
            int status = nhanVienService.CapNhatNhanVien(nhanVien);
            if (status == 0)
            {
                logger.Info("Status: Success");
                return RedirectToAction("XemNhanVien", "NhanVien");
            }
            else
            {
                logger.Info("Status: Fail");
                return Content("Thất bại");
            }
        }
        //GET: 
        public ActionResult XacNhanXoa(int id)
        {
            return View(nhanVienService.XemNhanVienNV(id));
        }
        //POST:
        public ActionResult XoaNhanVien(NhanVien nhanVien)
        {
            logger.Info("Start controller....");
            IList<TaiKhoanNV> listTK = new List<TaiKhoanNV>();
            listTK = taiKhoanNVService.XemTaiKhoan();
            int statusTK = 0;
            for(int i = 0; i < listTK.Count(); i++)
            {
                if(listTK[i].maNV == nhanVien.maNV)
                {
                    statusTK = taiKhoanNVService.XoaTaiKhoan(listTK[i].maNV);
                    break;
                }
            }
            int status = nhanVienService.XoaNhanVien(nhanVien.maNV);
            if (status == 0 && statusTK == 0)
            {
                logger.Info("Status: Success");
                return RedirectToAction("XemNhanVien", "NhanVien");
            }
            else
            {
                logger.Info("Status: Fail");
                return Content(" thất bại");
            }
        }
    }
}