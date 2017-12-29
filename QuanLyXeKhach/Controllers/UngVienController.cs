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
    public class UngVienController : Controller
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        IUngVienService<UngVien> ungVienService;
        ITrangThaiUVService<TrangThaiUV> trangThaiUVService;
        ILichPhongVanService<LichPhongVan> lichPhongVanService;

        public UngVienController(IUngVienService<UngVien> _ungVienService, ITrangThaiUVService<TrangThaiUV> _trangThaiUVService, ILichPhongVanService<LichPhongVan> _lichPhongVanService)
        {
            ungVienService = _ungVienService;
            trangThaiUVService = _trangThaiUVService;
            lichPhongVanService = _lichPhongVanService;
        }


        public ActionResult QuanLyUngVien(string filter = null, int page = 1,
         int pageSize = 5, string sort = "maUV", string sortdir = "DESC")
        {
            logger.Info("HttpGet recived. Contoller: UngVienController, ActionResult: QuanLyUngVien.");
            //ViewBag.LichPhongVans = new SelectList(lichPhongVanService.XemLichPhongVan(), "maLPV", "maLPV");
            //ViewBag.TrangThaiUVs = new SelectList(trangThaiUVService.XemTrangThaiUV(), "maTT", "tenTT");
            //var ungViens = ungVienService.XemThongTinUV();

            //foreach (var ungvien in ungViens)
            //{
            //    if (ungvien.maLPV == null)
            //        ungvien.LichPhongVan = null;
            //    else
            //        ungvien.LichPhongVan = lichPhongVanService.XemLichPhongVan(ungvien.maLPV ?? default(int));
            //    ungvien.TrangThaiUV = trangThaiUVService.XemTrangThaiUV(ungvien.trangThai);
            //}
            //logger.Info("/Return to action QuanLyUngVien with list of UngVien as model.");
            //return View(ungViens.ToList());
            var records = new PagedList<UngVien>();
            var notificationList = TempData["notificationList"] as IList<Notify>;
            ViewBag.filter = filter;
            records.Content = ungVienService.XemThongTinUV(filter).ToList();

            // Count
            records.TotalRecords = records.Content.Count();

            records.CurrentPage = page;
            records.PageSize = pageSize;

            foreach (var ungVien in records.Content)
            {
                if (ungVien.maLPV == null)
                    ungVien.LichPhongVan = null;
                else
                    ungVien.LichPhongVan = lichPhongVanService.XemThongTinLPV(ungVien.maLPV ?? default(int));
                ungVien.TrangThaiUV = trangThaiUVService.XemTrangThaiUV(ungVien.trangThai);
            }
            logger.Info("/Return to action QuanLyUngVien with list of UngVien as model.");
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
            logger.Info("HttpGet recived. Contoller: UngVienController, ActionResult: PVConfirmDelete, Id: " + id.ToString() + ".");
            if (id == null)
            {
                logger.Info("/tId is null.");
                IList<Notify> notificationList = new List<Notify>();
                notificationList.Add(new Notify("Mã ứng viên rỗng.", NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyUngVien");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UngVien ungVien = ungVienService.XemThongTinUV(id ?? default(int));
            if (ungVien == null)
            {
                logger.Info("/tId not found.");
                //return this.RedirectToAction(c => c.);
                IList<Notify> notificationList = new List<Notify>();
                notificationList.Add(new Notify(String.Format("Ứng viên không tồn tại hoặc đã xóa."), NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyUngVien");
            }

            logger.Info("/tReturn partial view PVConfirmDelete.");
            return PartialView("PVConfirmDelete", ungVien);
        }

        [HttpPost, ActionName("PVConfirmDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult PVConfirmDelete(int id)
        {
            logger.Info("HttpPost recived. Contoller: UngVienController, ActionResult: PVConfirmDelete, Id: " + id.ToString() + ".");
            UngVien ungVien = ungVienService.XemThongTinUV(id);
            IList<Notify> notificationList = new List<Notify>();
            if (ungVien == null)
            {
                logger.Info("/tId not found.");
                notificationList.Add(new Notify(String.Format("Ứng viên không tồn tại hoặc đã xóa. Mã ứng viên {0}.", ungVien.maUV), NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyUngVien");
            }
            int status = ungVienService.XoaUngVien(id);
            if (status == 0)
            {
                logger.Info("/t.Delete successfully.");
                //ViewBag.msgType = "sussces";
                //ViewBag.msg = "Xóa ứng viên thành công.";
                notificationList.Add(new Notify(String.Format("Xóa ứng viên thành công. Mã ứng viên {0}.", ungVien.maUV), NotificationType.SUCCESS));
            }
            else if (status < 0)
            {
                logger.Info("/t.Delete unsuccessfully.");
                //ViewBag.msgType = "warning"; ViewBag.msgTitle = "Lỗi!";
                //ViewBag.msg = "Xóa ứng viên thất bại.";
                notificationList.Add(new Notify(String.Format("Xóa ứng viên thất bại. Mã ứng viên {0}.", ungVien.maUV), NotificationType.ERROR));
            }
            else if (status > 0)
            {
                logger.Info("/t.Deleted with warning.");
                //ViewBag.msgType = "warning"; ViewBag.msgTitle = "Cảnh báo!";
                //ViewBag.msg = "Ứng viên đã được xóa.";
                notificationList.Add(new Notify(String.Format("Ứng viên đã được xóa. Mã ứng viên {0}.", ungVien.maUV), NotificationType.WARNING));
            }

            logger.Info("/tRedirect to action QuanLyUngVien.");
            TempData["notificationList"] = notificationList;
            return RedirectToAction("QuanLyUngVien");
        }

        //// GET: UngVien
        //public ActionResult Index()
        //{
        //    var ungVien = db.UngViens.Include(u => u.LichPhongVan).Include(t => t.TrangThaiUV);
        //    return View(ungVien.ToList());
        //}

        // GET: UngVien/Details/5
        public ActionResult PVDetails(int? id)
        {
            logger.Info("HttpGet recived. Contoller: UngVienController, ActionResult: PVDetails, Id: " + id.ToString() + ".");
            if (id == null)
            {
                logger.Info("/tId is null.");
                IList<Notify> notificationList = new List<Notify>();
                notificationList.Add(new Notify("Mã ứng viên rỗng.", NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyUngVien");
            }

            UngVien ungVien = ungVienService.XemThongTinUV(id ?? default(int));
            if (ungVien == null)
            {
                logger.Info("/tId not found.");
                IList<Notify> notificationList = new List<Notify>();
                notificationList.Add(new Notify(String.Format("Ứng viên không tồn tại hoặc đã xóa. Mã ứng viên {0}.", ungVien.maUV), NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyUngVien");
            }
            logger.Info("/tReturn partial view PVDetails.");
            ungVien.TrangThaiUV = trangThaiUVService.XemTrangThaiUV(ungVien.trangThai);
            return PartialView("PVDetails", ungVien);
        }

        // GET: UngVien/Create
        public ActionResult PVCreate()
        {
            logger.Info("HttpGet recived. Contoller: UngVienController, ActionResult: PVCreate.");

            ViewBag.LichPhongVans = new SelectList(lichPhongVanService.XemThongTinLPV(), "maLPV", "displayName");
            ViewBag.TrangThaiUVs = new SelectList(trangThaiUVService.XemTrangThaiUV(), "maTT", "tenTT");

            logger.Info("/tReturn partial view PVCreate.");
            return PartialView("PVCreate");
        }

        // GET: UngVien/Create
        //public ActionResult PVCreate(UngVien ungVien)
        //{
        //    ViewBag.LichPhongVans = new SelectList(db.LichPhongVans, "maLPV", "maLPV");
        //    ViewBag.TrangThaiUVs = new SelectList(db.TrangThaiUVs, "maTT", "tenTT");
        //    return PartialView("PVCreate", ungVien);
        //}

        // POST: UngVien/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PVCreate([Bind(Include = "maUV,hoTen,sDT,email,trangThai,maLPV")] UngVien ungVien)
        {
            logger.Info("HttpPost recived. Contoller: UngVienController, ActionResult: PVCreate." +
                " hoTen = " + ungVien.hoTen +
                " sDT = " + ungVien.sDT +
                " email = " + ungVien.email +
                " trangThai = " + ungVien.trangThai +
                " maLPV = " + ungVien.maLPV
                );
            //ungVien.TrangThaiUV = trangThaiUVService.XemTrangThaiUV(ungVien.trangThai);
            //if (ungVien.maLPV == null)
            //    ungVien.LichPhongVan = null;
            //else
            //    ungVien.LichPhongVan = lichPhongVanService.XemLichPhongVan(ungVien.maLPV ?? default(int));

            //ungVien.maUV = 0;

            int status = ungVienService.ThemUngVien(ungVien);
            IList<Notify> notificationList = new List<Notify>();
            if (status == 0)
            {
                logger.Info("/t.Create successfully.");
                //ViewBag.msgType = "sussces";
                //ViewBag.msg = "Thêm ứng viên thành công.";
                notificationList.Add(new Notify(String.Format("Thêm ứng viên thành công. Mã ứng viên {0}.", ungVien.maUV), NotificationType.SUCCESS));
            }
            else if (status < 0)
            {
                logger.Info("/t.Create unsuccessfully.");
                //ViewBag.msgType = "warning"; ViewBag.msgTitle = "Lỗi!";
                //ViewBag.msg = "Thêm ứng viên không thành công.";
                notificationList.Add(new Notify("Thêm ứng viên không thành công.", NotificationType.ERROR));
            }
            else if (status > 0)
            {
                logger.Info("/t.Deleted with warning.");
                //ViewBag.msgType = "warning"; ViewBag.msgTitle = "Cảnh báo!";
                //ViewBag.msg = "Ứng viên đã được thêm.";
                notificationList.Add(new Notify(String.Format("Ứng viên đã được thêm. Mã ứng viên {0}.", ungVien.maUV), NotificationType.WARNING));
            }
            logger.Info("/tReturn QuanLyUngVien ActionResult.");


            TempData["notificationList"] = notificationList;
            return RedirectToAction("QuanLyUngVien");
            //if (ModelState.IsValid)
            //{
            //    db.UngViens.Add(ungVien);
            //    db.SaveChanges();
            //    return RedirectToAction("QuanLyUngVien");
            //}
            //ViewBag.LichPhongVans = new SelectList(db.LichPhongVans, "maLPV", "maLPV");
            //ViewBag.TrangThaiUVs = new SelectList(db.TrangThaiUVs, "maTT", "tenTT", 2);
            //return PartialView("PVCreate", ungVien);
        }

        // GET: UngVien/Edit/5
        public ActionResult PVEdit(int? id)
        {
            logger.Info("HttpGet recived. Contoller: UngVienController, ActionResult: PVEdit.");
            if (id == null)
            {
                logger.Info("/tId is null.");
                IList<Notify> notificationList = new List<Notify>();
                notificationList.Add(new Notify("Mã ứng viên rỗng.", NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyUngVien");
            }
            UngVien ungVien = ungVienService.XemThongTinUV(id ?? default(int));
            if (ungVien == null)
            {
                logger.Info("/tId not found.");
                IList<Notify> notificationList = new List<Notify>();
                notificationList.Add(new Notify(String.Format("Ứng viên không tồn tại hoặc đã xóa. Mã ứng viên {0}.", ungVien.maUV), NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyUngVien");
            }


            ViewBag.LichPhongVans = new SelectList(lichPhongVanService.XemThongTinLPV(), "maLPV", "displayName");
            ViewBag.TrangThaiUVs = new SelectList(trangThaiUVService.XemTrangThaiUV(), "maTT", "tenTT");

            logger.Info("/tReturn partial view PVEdit.");
            return PartialView("PVEdit", ungVien);
        }

        // POST: UngVien/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PVEdit([Bind(Include = "maUV,hoTen,sDT,email,trangThai,maLPV")] UngVien ungVien)
        {
            logger.Info("HttpPost recived. Contoller: UngVienController, ActionResult: PVEdit." +
                " hoTen = " + ungVien.hoTen +
                " sDT = " + ungVien.sDT +
                " email = " + ungVien.email +
                " trangThai = " + ungVien.trangThai +
                " maLPV = " + ungVien.maLPV
                );
            IList<Notify> notificationList = new List<Notify>();
            if (ModelState.IsValid)
            {
                if (ungVien.maLPV == null)
                {
                    logger.Info("/tmaLPV is null.");
                }
                int status = ungVienService.CapNhatThongTinUV(ungVien);

                if (status == 0)
                {
                    logger.Info("/t.Update successfully.");
                    //ViewBag.msgType = "sussces";
                    //ViewBag.msg = "Cập nhật ứng viên thành công.";
                    notificationList.Add(new Notify(String.Format("Cập ứng viên nhật thành công. Mã ứng viên {0}.", ungVien.maUV), NotificationType.SUCCESS));
                }
                else if (status < 0)
                {
                    logger.Info("/t.Update unsuccessfully.");
                    //ViewBag.msgType = "warning"; ViewBag.msgTitle = "Lỗi!";
                    //ViewBag.msg = "Cập nhật ứng viên không thành công.";
                    notificationList.Add(new Notify(String.Format("Cập nhật ứng viên không thành công. Mã ứng viên {0}.", ungVien.maUV), NotificationType.ERROR));
                }
                else if (status > 0)
                {
                    logger.Info("/t.Updated with warning.");
                    //ViewBag.msgType = "warning"; ViewBag.msgTitle = "Cảnh báo!";
                    //ViewBag.msg = "Ứng viên đã được cập nhật.";
                    notificationList.Add(new Notify(String.Format("Ứng viên đã được cập nhật. Mã ứng viên {0}.", ungVien.maUV), NotificationType.WARNING));
                }

                logger.Info("/tReturn QuanLyUngVien ActionResult.");
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyUngVien");
            }
            //ViewBag.LichPhongVans = new SelectList(lichPhongVanService.XemThongTinLPV(), "maLPV", "maLPV");
            //ViewBag.TrangThaiUVs = new SelectList(trangThaiUVService.XemTrangThaiUV(), "maTT", "tenTT");
            notificationList.Add(new Notify("Thông tin ứng viên không hợp lệ.", NotificationType.ERROR));
            TempData["notificationList"] = notificationList;
            return RedirectToAction("QuanLyUngVien");
        }

        // GET: UngVien/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    logger.Info("HttpGet recived. Contoller: UngVienController, ActionResult: PVEdit." +
        //   if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    UngVien ungVien = ungVienService.XemThongTinUV(id);
        //    if (ungVien == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(ungVien);
        //}

        // POST: UngVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //UngVien ungVien = db.UngViens.Find(id);
            //db.UngViens.Remove(ungVien);
            //db.SaveChanges();
            var view = QuanLyUngVien();

            return view;
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
