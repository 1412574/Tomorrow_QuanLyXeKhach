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

namespace QuanLyXeKhach.Controllers
{
    public class BangChamCongController : Controller
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        IBangChamCongService<BangChamCong> bangChamCongService;
        INhanVienService<NhanVien> nhanVienService;

        public BangChamCongController(IBangChamCongService<BangChamCong> _bangChamCongService, INhanVienService<NhanVien> _nhanVienService)
        {
            bangChamCongService = _bangChamCongService;
            nhanVienService = _nhanVienService;
        }


        public ActionResult QuanLyBangChamCong(string filter = null)
        {
            logger.Info("HttpGet recived. Contoller: BangChamCongController, ActionResult: QuanLyBCC.");
            var records = new List<BangChamCong>();
            var notificationList = TempData["notificationList"] as IList<Notify>;
            ViewBag.filter = filter;

            foreach (var bCC in records)
            {
                bCC.NhanVien = nhanVienService.XemNhanVienNV(bCC.maNV);
            }
            logger.Info("/Return to action QuanLyBCC with list of BangChamCong as model.");
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
            logger.Info("HttpGet recived. Contoller: BangChamCongController, ActionResult: PVConfirmDelete, Id: " + id.ToString() + ".");
            if (id == null)
            {
                logger.Info("/tId is null.");
                IList<Notify> notificationList = new List<Notify>();
                notificationList.Add(new Notify("Mã bảng chấm công rỗng.", NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyBangChamCong");
            }
            BangChamCong bangChamCong = bangChamCongService.XemThongTinBCC(id ?? default(int));
            if (bangChamCong == null)
            {
                logger.Info("/tId not found.");
                IList<Notify> notificationList = new List<Notify>();
                notificationList.Add(new Notify(String.Format("Bảng chấm công không tồn tại hoặc đã xóa. Mã bảng chấm công {0}.", bangChamCong.maCC), NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyBangChamCong");
            }

            logger.Info("/tReturn partial view PVConfirmDelete.");
            bangChamCong.NhanVien = nhanVienService.XemNhanVienNV(bangChamCong.maNV);
            return PartialView("PVConfirmDelete", bangChamCong);
        }

        [HttpPost, ActionName("PVConfirmDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult PVConfirmDelete(int id)
        {
            logger.Info("HttpPost recived. Contoller: BangChamCongController, ActionResult: PVConfirmDelete, Id: " + id.ToString() + ".");
            BangChamCong bangChamCong = bangChamCongService.XemThongTinBCC(id);
            IList<Notify> notificationList = new List<Notify>();
            if (bangChamCong == null)
            {
                logger.Info("/tId not found.");
                notificationList.Add(new Notify(String.Format("Bảng chấm công không tồn tại hoặc đã xóa. Mã bảng chấm công {0}.", bangChamCong.maCC), NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyBangChamCong");
            }
            int status = bangChamCongService.XoaBangChamCong(id);
            if (status == 0)
            {
                logger.Info("/t.Delete successfully.");
                notificationList.Add(new Notify(String.Format("Xóa bảng chấm công thành công. Mã bảng chấm công {0}.", bangChamCong.maCC), NotificationType.SUCCESS));
            }
            else if (status < 0)
            {
                logger.Info("/t.Delete unsuccessfully.");
                notificationList.Add(new Notify(String.Format("Xóa bảng chấm công thất bại. Mã bảng chấm công {0}.", bangChamCong.maCC), NotificationType.ERROR));
            }
            else if (status > 0)
            {
                logger.Info("/t.Deleted with warning.");
                notificationList.Add(new Notify(String.Format("Bảng chấm công đã được xóa. Mã bảng chấm công {0}.", bangChamCong.maCC), NotificationType.WARNING));
            }

            logger.Info("/tRedirect to action QuanLyBangChamCong.");
            TempData["notificationList"] = notificationList;
            return RedirectToAction("QuanLyBangChamCong");
        }

        // GET: BangChamCong/Details/5
        public ActionResult PVDetails(int? id)
        {
            logger.Info("HttpGet recived. Contoller: BangChamCongController, ActionResult: PVDetails, Id: " + id.ToString() + ".");
            if (id == null)
            {
                logger.Info("/tId is null.");
                IList<Notify> notificationList = new List<Notify>();
                notificationList.Add(new Notify("Mã bảng chấm công rỗng.", NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyBangChamCong");
            }

            BangChamCong bangChamCong = bangChamCongService.XemThongTinBCC(id ?? default(int));
            if (bangChamCong == null)
            {
                logger.Info("/tId not found.");
                IList<Notify> notificationList = new List<Notify>();
                notificationList.Add(new Notify(String.Format("Bảng chấm công không tồn tại hoặc đã xóa. Mã bảng chấm công {0}.", bangChamCong.maCC), NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyBangChamCong");
            }
            logger.Info("/tReturn partial view PVDetails.");
            bangChamCong.NhanVien = nhanVienService.XemNhanVienNV(bangChamCong.maNV);
            return PartialView("PVDetails", bangChamCong);
        }

        // GET: BangChamCong/Create
        public ActionResult PVCreate()
        {
            logger.Info("HttpGet recived. Contoller: BangChamCongController, ActionResult: PVCreate.");


            logger.Info("/tReturn partial view PVCreate.");
            return PartialView("PVCreate");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PVCreate([Bind(Include = "maCC,maNV,ngay,gioBatDau,gioKetThuc,ghiChu")] BangChamCong bangChamCong)
        {
            logger.Info("HttpPost recived. Contoller: BangChamCongController, ActionResult: PVCreate." +
                " maNV = " + bangChamCong.maNV +
                " ngay = " + bangChamCong.ngay +
                " gioBatDau = " + bangChamCong.gioBatDau +
                " gioKetThuc = " + bangChamCong.gioKetThuc +
                " ghiChu = " + bangChamCong.ghiChu
                );

            int status = bangChamCongService.ThemBangChamCong(bangChamCong);
            IList<Notify> notificationList = new List<Notify>();
            if (status == 0)
            {
                logger.Info("/t.Create successfully.");
                notificationList.Add(new Notify(String.Format("Thêm bảng chấm công thành công. Mã bảng chấm công {0}.", bangChamCong.maCC), NotificationType.SUCCESS));
            }
            else if (status < 0)
            {
                logger.Info("/t.Create unsuccessfully.");
                notificationList.Add(new Notify("Thêm bảng chấm công không thành công.", NotificationType.ERROR));
            }
            else if (status > 0)
            {
                logger.Info("/t.Deleted with warning.");
                notificationList.Add(new Notify(String.Format("Bảng chấm công đã được thêm. Mã bảng chấm công {0}.", bangChamCong.maCC), NotificationType.WARNING));
            }
            logger.Info("/tRedirect to action QuanLyBangChamCong.");
            TempData["notificationList"] = notificationList;
            return RedirectToAction("QuanLyBangChamCong");
        }

        // GET: BangChamCong/Edit/5
        public ActionResult PVEdit(int? id)
        {
            logger.Info("HttpGet recived. Contoller: BangChamCongController, ActionResult: PVEdit.");
            if (id == null)
            {
                logger.Info("/tId is null.");
                IList<Notify> notificationList = new List<Notify>();
                notificationList.Add(new Notify("Mã bảng chấm công rỗng.", NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyBangChamCong");
            }
            BangChamCong bangChamCong = bangChamCongService.XemThongTinBCC(id ?? default(int));
            if (bangChamCong == null)
            {
                logger.Info("/tId not found.");
                IList<Notify> notificationList = new List<Notify>();
                notificationList.Add(new Notify(String.Format("Bảng chấm công không tồn tại hoặc đã xóa. Mã bảng chấm công {0}.", bangChamCong.maCC), NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyBangChamCong");
            }


            bangChamCong.NhanVien = nhanVienService.XemNhanVienNV(bangChamCong.maNV);

            logger.Info("/tReturn partial view PVEdit.");
            return PartialView("PVEdit", bangChamCong);
        }

        // POST: BangChamCong/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PVEdit([Bind(Include = "maCC,maNV,ngay,gioBatDau,gioKetThuc,ghiChu")] BangChamCong bangChamCong)
        {
            logger.Info("HttpPost recived. Contoller: BangChamCongController, ActionResult: PVEdit." +
                " maNV = " + bangChamCong.maNV +
                " ngay = " + bangChamCong.ngay +
                " gioBatDau = " + bangChamCong.gioBatDau +
                " gioKetThuc = " + bangChamCong.gioKetThuc +
                " ghiChu = " + bangChamCong.ghiChu
                );
            IList<Notify> notificationList = new List<Notify>();
            if (ModelState.IsValid)
            {
                int status = bangChamCongService.CapNhatThongTinBCC(bangChamCong);

                if (status == 0)
                {
                    logger.Info("/t.Update successfully.");
                    notificationList.Add(new Notify(String.Format("Cập nhật bảng chấm công thành công. Mã bảng chấm công {0}.", bangChamCong.maCC), NotificationType.SUCCESS));
                }
                else if (status < 0)
                {
                    logger.Info("/t.Update unsuccessfully.");
                    notificationList.Add(new Notify(String.Format("Cập nhật bảng chấm công không thành công. Mã bảng chấm công {0}.", bangChamCong.maCC), NotificationType.ERROR));
                }
                else if (status > 0)
                {
                    logger.Info("/t.Updated with warning.");
                    notificationList.Add(new Notify(String.Format("Bảng chấm công đã được cập nhật. Mã bảng chấm công {0}.", bangChamCong.maCC), NotificationType.WARNING));
                }

                logger.Info("/tRedirect to action QuanLyBangChamCong.");
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyBangChamCong");
            }
            notificationList.Add(new Notify("Thông tin bảng chấm công không hợp lệ.", NotificationType.ERROR));
            TempData["notificationList"] = notificationList;
            return RedirectToAction("QuanLyBangChamCong");
        }

        // POST: BangChamCong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //BangChamCong bangChamCong = db.BangChamCongs.Find(id);
            //db.BangChamCongs.Remove(bangChamCong);
            //db.SaveChanges();
            return RedirectToAction("QuanLyBangChamCong");
        }

    }
}
