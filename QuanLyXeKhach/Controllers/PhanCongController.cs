using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataModel;
using NLog;
using DataService;
using QuanLyXeKhach.Extensions;
using System.Dynamic;

namespace QuanLyXeKhach.Controllers
{
    public class PhanCongController : Controller
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        IPhanCongService<PhanCong, NhanVien, CongViec> phanCongService;
        INhanVienService<NhanVien> nhanVienService;
        ICongViecService<CongViec> congViecService;
        ITrangThaiNVService<TrangThaiNV> trangThaiNVService;
        IVaiTroService<VaiTro> vaiTroService;

        public PhanCongController(IPhanCongService<PhanCong, NhanVien, CongViec> _phanCongService, INhanVienService<NhanVien> _nhanVienService, ICongViecService<CongViec> _congViecService, ITrangThaiNVService<TrangThaiNV> _trangThaiNVService, IVaiTroService<VaiTro> _vaiTroService)
        {
            phanCongService = _phanCongService;
            nhanVienService = _nhanVienService;
            congViecService = _congViecService;
            trangThaiNVService = _trangThaiNVService;
            vaiTroService = _vaiTroService;
        }


        public ActionResult QuanLyPhanCong(string filter = null)
        {
            logger.Info("HttpGet recived. Contoller: PhanCongController, ActionResult: QuanLyPC.");
            var records = new List<PhanCong>();
            var notificationList = TempData["notificationList"] as IList<Notify>;
            ViewBag.filter = filter;
            records = phanCongService.XemThongTinPC(filter).ToList();

            foreach (var phanCong in records)
            {
                phanCong.NhanVien = nhanVienService.XemNhanVienNV(phanCong.maNV);
                phanCong.CongViec = congViecService.XemThongTinCV(phanCong.maCV);
            }
            logger.Info("/Return to action QuanLyPC with list of PhanCong as model.");
            if (notificationList != null)
            {
                foreach (Notify n in notificationList)
                {
                    this.AddNotification(n.message, n.notificationType);
                }
            }
            return View(records);
        }
        public ActionResult PVConfirmDelete(int? id)
        {
            logger.Info("HttpGet recived. Contoller: PhanCongController, ActionResult: PVConfirmDelete, Id: " + id.ToString() + ".");
            if (id == null)
            {
                logger.Info("/tId is null.");
                IList<Notify> notificationList = new List<Notify>();
                notificationList.Add(new Notify("Mã phân công rỗng.", NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyPhanCong");
            }
            PhanCong phanCong = phanCongService.XemThongTinPC(id ?? default(int));
            if (phanCong == null)
            {
                logger.Info("/tId not found.");
                IList<Notify> notificationList = new List<Notify>();
                notificationList.Add(new Notify(String.Format("Phân công không tồn tại hoặc đã xóa. Mã phân nhân viên {0}, mã công việc {1}.", phanCong.maNV, phanCong.maCV), NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyPhanCong");
            }

            phanCong.NhanVien = nhanVienService.XemNhanVienNV(phanCong.maNV);
            phanCong.CongViec = congViecService.XemThongTinCV(phanCong.maCV);
            logger.Info("/tReturn partial view PVConfirmDelete.");
            return PartialView("PVConfirmDelete", phanCong);
        }

        [HttpPost, ActionName("PVConfirmDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult PVConfirmDelete(int id)
        {
            logger.Info("HttpPost recived. Contoller: PhanCongController, ActionResult: PVConfirmDelete, Id: " + id.ToString() + ".");
            PhanCong phanCong = phanCongService.XemThongTinPC(id);
            IList<Notify> notificationList = new List<Notify>();
            if (phanCong == null)
            {
                logger.Info("/tId not found.");
                notificationList.Add(new Notify(String.Format("Phân công không tồn tại hoặc đã xóa. Mã phân nhân viên {0}, mã công việc {1}.", phanCong.maNV, phanCong.maCV), NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyPhanCong");
            }
            int status = phanCongService.XoaPhanCong(id);
            if (status == 0)
            {
                logger.Info("/t.Delete successfully.");
                notificationList.Add(new Notify(String.Format("Xóa phân công thành công. Mã phân nhân viên {0}, mã công việc {1}.", phanCong.maNV, phanCong.maCV), NotificationType.SUCCESS));
            }
            else if (status < 0)
            {
                logger.Info("/t.Delete unsuccessfully.");
                notificationList.Add(new Notify(String.Format("Xóa phân công thất bại. Mã phân nhân viên {0}, mã công việc {1}.", phanCong.maNV, phanCong.maCV), NotificationType.ERROR));
            }
            else if (status > 0)
            {
                logger.Info("/t.Deleted with warning.");
                notificationList.Add(new Notify(String.Format("Phân công đã được xóa. Mã phân nhân viên {0}, mã công việc {1}.", phanCong.maNV, phanCong.maCV), NotificationType.WARNING));
            }

            logger.Info("/tRedirect to action QuanLyPhanCong.");
            TempData["notificationList"] = notificationList;
            return RedirectToAction("QuanLyPhanCong");
        }

        // GET: PhanCong/Details/5
        public ActionResult PVDetails(int? id)
        {
            logger.Info("HttpGet recived. Contoller: PhanCongController, ActionResult: PVDetails, Id: " + id.ToString() + ".");
            if (id == null)
            {
                logger.Info("/tId is null.");
                IList<Notify> notificationList = new List<Notify>();
                notificationList.Add(new Notify("Mã phân công rỗng.", NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyPhanCong");
            }

            PhanCong phanCong = phanCongService.XemThongTinPC(id ?? default(int));
            if (phanCong == null)
            {
                logger.Info("/tId not found.");
                IList<Notify> notificationList = new List<Notify>();
                notificationList.Add(new Notify(String.Format("Phân công không tồn tại hoặc đã xóa. Mã phân nhân viên {0}, mã công việc {1}.", phanCong.maNV, phanCong.maCV), NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyPhanCong");
            }
            logger.Info("/tReturn partial view PVDetails.");
            return PartialView("PVDetails", phanCong);
        }

        // GET: PhanCong/Create
        public ActionResult PVCreate()
        {
            logger.Info("HttpGet recived. Contoller: PhanCongController, ActionResult: PVCreate.");

            IEnumerable<NhanVien> nhanViens = nhanVienService.XemNhanVien();
            foreach(var nhanVien in nhanViens)
            {
                nhanVien.TrangThai = trangThaiNVService.XemTrangThaiNV(nhanVien.maTT);
                nhanVien.VaiTro = vaiTroService.XemVaiTroNV(nhanVien.maVT);
            }
            ViewData["NhanViens"] = nhanVienService.XemNhanVien();
            ViewData["CongViecs"] = congViecService.XemThongTinCV();
            //dynamic mymodel = new ExpandoObject();
            //mymodel.CongViecs = congViecService.XemThongTinCV();
            //mymodel.NhanViens = nhanVienService.XemNhanVien();
            logger.Info("/tReturn partial view PVCreate.");
            return PartialView("PVCreate");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PVCreate([Bind(Include = "maPC,maNV,maCV,ngayPC,danhGia,ghiChu,nhiemVu")] PhanCong phanCong)
        {
            logger.Info("HttpPost recived. Contoller: PhanCongController, ActionResult: PVCreate." +
                " maPC = " + phanCong.maPC +
                " maNV = " + phanCong.maNV +
                " maCV = " + phanCong.maCV +
                " ngayPC = " + phanCong.ngayPC +
                " danhGia = " + phanCong.danhGia + 
                " ghiChu = " + phanCong.ghiChu +
                " nhiemVu = " + phanCong.nhiemVu
                );
            int status = phanCongService.ThemPhanCong(phanCong);
            IList<Notify> notificationList = new List<Notify>();
            if (status == 0)
            {
                logger.Info("/t.Create successfully.");
                notificationList.Add(new Notify(String.Format("Thêm phân công thành công. Mã phân nhân viên {0}, mã công việc {1}.", phanCong.maNV, phanCong.maCV), NotificationType.SUCCESS));
            }
            else if (status < 0)
            {
                logger.Info("/t.Create unsuccessfully.");
                notificationList.Add(new Notify("Thêm phân công không thành công.", NotificationType.ERROR));
            }
            else if (status > 0)
            {
                logger.Info("/t.Deleted with warning.");
                notificationList.Add(new Notify(String.Format("Phân công đã được thêm. Mã phân nhân viên {0}, mã công việc {1}.", phanCong.maNV, phanCong.maCV), NotificationType.WARNING));
            }
            logger.Info("/tRedirect to action QuanLyPhanCong.");
            TempData["notificationList"] = notificationList;
            return RedirectToAction("QuanLyPhanCong");

        }

        // GET: PhanCong/Edit/5
        public ActionResult PVEdit(int? id)
        {
            logger.Info("HttpGet recived. Contoller: PhanCongController, ActionResult: PVEdit.");
            if (id == null)
            {
                logger.Info("/tId is null.");
                IList<Notify> notificationList = new List<Notify>();
                notificationList.Add(new Notify("Mã phân công rỗng.", NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyPhanCong");
            }
            PhanCong phanCong = phanCongService.XemThongTinPC(id ?? default(int));
            if (phanCong == null)
            {
                logger.Info("/tId not found.");
                IList<Notify> notificationList = new List<Notify>();
                notificationList.Add(new Notify(String.Format("Phân công không tồn tại hoặc đã xóa. Mã phân nhân viên {0}, mã công việc {1}.", phanCong.maNV, phanCong.maCV), NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyPhanCong");
            }


            IEnumerable<NhanVien> nhanViens = nhanVienService.XemNhanVien();
            foreach (var nhanVien in nhanViens)
            {
                nhanVien.TrangThai = trangThaiNVService.XemTrangThaiNV(nhanVien.maTT);
                nhanVien.VaiTro = vaiTroService.XemVaiTroNV(nhanVien.maVT);
            }
            ViewData["NhanViens"] = nhanVienService.XemNhanVien();
            ViewData["CongViecs"] = congViecService.XemThongTinCV();
            logger.Info("/tReturn partial view PVEdit.");
            return PartialView("PVEdit", phanCong);
        }

        // POST: PhanCong/PVEdit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PVEdit([Bind(Include = "maPC,maNV,maCV,ngayPC,danhGia,ghiChu,nhiemVu")] PhanCong phanCong)
        {
            logger.Info("HttpPost recived. Contoller: PhanCongController, ActionResult: PVEdit." +
                " maPC = " + phanCong.maPC +
                " maNV = " + phanCong.maNV +
                " maCV = " + phanCong.maCV +
                " ngayPC = " + phanCong.ngayPC +
                " danhGia = " + phanCong.danhGia +
                " ghiChu = " + phanCong.ghiChu +
                " nhiemVu = " + phanCong.nhiemVu
                );
            IList<Notify> notificationList = new List<Notify>();
            if (ModelState.IsValid)
            {
                int status = phanCongService.CapNhatThongTinPC(phanCong);

                if (status == 0)
                {
                    logger.Info("/t.Update successfully.");
                    notificationList.Add(new Notify(String.Format("Cập nhật phân công thành công. Mã phân nhân viên {0}, mã công việc {1}.", phanCong.maNV, phanCong.maCV), NotificationType.SUCCESS));
                }
                else if (status < 0)
                {
                    logger.Info("/t.Update unsuccessfully.");
                    notificationList.Add(new Notify(String.Format("Cập nhật phân công không thành công. Mã phân nhân viên {0}, mã công việc {1}.", phanCong.maNV, phanCong.maCV), NotificationType.ERROR));
                }
                else if (status > 0)
                {
                    logger.Info("/t.Updated with warning.");
                    notificationList.Add(new Notify(String.Format("Phân công đã được cập nhật. Mã phân nhân viên {0}, mã công việc {1}.", phanCong.maNV, phanCong.maCV), NotificationType.WARNING));
                }

                logger.Info("/tRedirect to action QuanLyPhanCong.");
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyPhanCong");
            }
            notificationList.Add(new Notify("Thông tin phân công không hợp lệ.", NotificationType.ERROR));
            TempData["notificationList"] = notificationList;
            return RedirectToAction("QuanLyPhanCong");
        }

    }
}
