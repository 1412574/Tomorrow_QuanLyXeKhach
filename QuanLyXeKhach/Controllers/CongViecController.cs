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
    public class CongViecController : Controller
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        ICongViecService<CongViec> congViecService;

        public CongViecController(ICongViecService<CongViec> _congViecService)
        {
            congViecService = _congViecService;
        }


        public ActionResult QuanLyCongViec(string filter = null, int page = 1,
         int pageSize = 5, string sort = "maCV", string sortdir = "DESC")
        {
            logger.Info("HttpGet recived. Contoller: CongViecController, ActionResult: QuanLyCongViec.");
            //ViewBag.LichPhongVans = new SelectList(lichPhongVanService.XemLichPhongVan(), "maCV", "maCV");
            //ViewBag.TrangThaiCVs = new SelectList(trangThaiCVService.XemTrangThaiCV(), "maTT", "tenTT");
            //var congViecs = congViecService.XemThongTinCV();

            //foreach (var ungvien in congViecs)
            //{
            //    if (ungvien.maCV == null)
            //        ungvien.LichPhongVan = null;
            //    else
            //        ungvien.LichPhongVan = lichPhongVanService.XemLichPhongVan(ungvien.maCV ?? default(int));
            //    ungvien.TrangThaiCV = trangThaiCVService.XemTrangThaiCV(ungvien.trangThai);
            //}
            //logger.Info("/Return to action QuanLyCongViec with list of CongViec as model.");
            //return View(congViecs.ToList());
            var records = new PagedList<CongViec>();
            var notificationList = TempData["notificationList"] as IList<Notify>;
            ViewBag.filter = filter;
            records.Content = congViecService.XemThongTinCV(filter).ToList();

            // Count
            records.TotalRecords = records.Content.Count();

            records.CurrentPage = page;
            records.PageSize = pageSize;

            logger.Info("/Return to action QuanLyCongViec with list of CongViec as model.");
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
            logger.Info("HttpGet recived. Contoller: CongViecController, ActionResult: PVConfirmDelete, Id: " + id.ToString() + ".");
            if (id == null)
            {
                logger.Info("/tId is null.");
                IList<Notify> notificationList = new List<Notify>();
                notificationList.Add(new Notify("Mã công việc rỗng.", NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyCongViec");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CongViec congViec = congViecService.XemThongTinCV(id ?? default(int));
            if (congViec == null)
            {
                logger.Info("/tId not found.");
                //return this.RedirectToAction(c => c.);
                IList<Notify> notificationList = new List<Notify>();
                notificationList.Add(new Notify(String.Format("Công việc không tồn tại hoặc đã xóa. Mã công việc {0}.", congViec.maCV), NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyCongViec");
            }

            logger.Info("/tReturn partial view PVConfirmDelete.");
            return PartialView("PVConfirmDelete", congViec);
        }

        [HttpPost, ActionName("PVConfirmDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult PVConfirmDelete(int id)
        {
            logger.Info("HttpPost recived. Contoller: CongViecController, ActionResult: PVConfirmDelete, Id: " + id.ToString() + ".");
            CongViec congViec = congViecService.XemThongTinCV(id);
            IList<Notify> notificationList = new List<Notify>();
            if (congViec == null)
            {
                logger.Info("/tId not found.");
                notificationList.Add(new Notify(String.Format("Công việc không tồn tại hoặc đã xóa. Mã công việc {0}.", congViec.maCV), NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyCongViec");
            }
            int status = congViecService.XoaCongViec(id);
            if (status == 0)
            {
                logger.Info("/t.Delete successfully.");
                //ViewBag.msgType = "sussces";
                //ViewBag.msg = "Xóa công việc thành công.";
                notificationList.Add(new Notify(String.Format("Xóa công việc thành công. Mã công việc {0}.", congViec.maCV), NotificationType.SUCCESS));
            }
            else if (status < 0)
            {
                logger.Info("/t.Delete unsuccessfully.");
                //ViewBag.msgType = "warning"; ViewBag.msgTitle = "Lỗi!";
                //ViewBag.msg = "Xóa công việc thất bại.";
                notificationList.Add(new Notify(String.Format("Xóa công việc thất bại. Mã công việc {0}.", congViec.maCV), NotificationType.ERROR));
            }
            else if (status > 0)
            {
                logger.Info("/t.Deleted with warning.");
                //ViewBag.msgType = "warning"; ViewBag.msgTitle = "Cảnh báo!";
                //ViewBag.msg = "Công việc đã được xóa.";
                notificationList.Add(new Notify(String.Format("Công việc đã được xóa. Mã công việc {0}.", congViec.maCV), NotificationType.WARNING));
            }

            logger.Info("/tRedirect to action QuanLyCongViec.");
            TempData["notificationList"] = notificationList;
            return RedirectToAction("QuanLyCongViec");
        }

        //// GET: CongViec
        //public ActionResult Index()
        //{
        //    var congViec = db.CongViecs.Include(u => u.LichPhongVan).Include(t => t.TrangThaiCV);
        //    return View(congViec.ToList());
        //}

        // GET: CongViec/Details/5
        public ActionResult PVDetails(int? id)
        {
            logger.Info("HttpGet recived. Contoller: CongViecController, ActionResult: PVDetails, Id: " + id.ToString() + ".");
            if (id == null)
            {
                logger.Info("/tId is null.");
                IList<Notify> notificationList = new List<Notify>();
                notificationList.Add(new Notify("Mã công việc rỗng.", NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyCongViec");
            }

            CongViec congViec = congViecService.XemThongTinCV(id ?? default(int));
            if (congViec == null)
            {
                logger.Info("/tId not found.");
                IList<Notify> notificationList = new List<Notify>();
                notificationList.Add(new Notify(String.Format("Công việc không tồn tại hoặc đã xóa. Mã công việc {0}.", congViec.maCV), NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyCongViec");
            }
            logger.Info("/tReturn partial view PVDetails.");
            return PartialView("PVDetails", congViec);
        }

        // GET: CongViec/Create
        public ActionResult PVCreate()
        {
            logger.Info("HttpGet recived. Contoller: CongViecController, ActionResult: PVCreate.");

            logger.Info("/tReturn partial view PVCreate.");
            return PartialView("PVCreate");
        }

        // GET: CongViec/Create
        //public ActionResult PVCreate(CongViec congViec)
        //{
        //    ViewBag.LichPhongVans = new SelectList(db.LichPhongVans, "maCV", "maCV");
        //    ViewBag.TrangThaiCVs = new SelectList(db.TrangThaiCVs, "maTT", "tenTT");
        //    return PartialView("PVCreate", congViec);
        //}

        // POST: CongViec/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PVCreate([Bind(Include = "maCV,tenCV,yeuCauCV,moTaCV,hanHoanThanh")] CongViec congViec)
        {
            logger.Info("HttpPost recived. Contoller: CongViecController, ActionResult: PVCreate." +
                " maCV = " + congViec.maCV +
                " tenCV = " + congViec.tenCV +
                " yeuCauCV = " + congViec.yeuCauCV +
                " moTaCV = " + congViec.moTaCV +
                " hanHoanThanh = " + congViec.hanHoanThanh
                );
            //congViec.TrangThaiCV = trangThaiCVService.XemTrangThaiCV(congViec.trangThai);
            //if (congViec.maCV == null)
            //    congViec.LichPhongVan = null;
            //else
            //    congViec.LichPhongVan = lichPhongVanService.XemLichPhongVan(congViec.maCV ?? default(int));

            //congViec.maCV = 0;

            int status = congViecService.ThemCongViec(congViec);
            IList<Notify> notificationList = new List<Notify>();
            if (status == 0)
            {
                logger.Info("/t.Create successfully.");
                //ViewBag.msgType = "sussces";
                //ViewBag.msg = "Thêm công việc thành công.";
                notificationList.Add(new Notify(String.Format("Thêm công việc thành công. Mã công việc {0}.", congViec.maCV), NotificationType.SUCCESS));
            }
            else if (status < 0)
            {
                logger.Info("/t.Create unsuccessfully.");
                //ViewBag.msgType = "warning"; ViewBag.msgTitle = "Lỗi!";
                //ViewBag.msg = "Thêm công việc không thành công.";
                notificationList.Add(new Notify("Thêm công việc không thành công.", NotificationType.ERROR));
            }
            else if (status > 0)
            {
                logger.Info("/t.Deleted with warning.");
                //ViewBag.msgType = "warning"; ViewBag.msgTitle = "Cảnh báo!";
                //ViewBag.msg = "Công việc đã được thêm.";
                notificationList.Add(new Notify(String.Format("Công việc đã được thêm. Mã công việc {0}.", congViec.maCV), NotificationType.WARNING));
            }
            logger.Info("/tReturn QuanLyCongViec ActionResult.");


            TempData["notificationList"] = notificationList;
            return RedirectToAction("QuanLyCongViec");
            //if (ModelState.IsValid)
            //{
            //    db.CongViecs.Add(congViec);
            //    db.SaveChanges();
            //    return RedirectToAction("QuanLyCongViec");
            //}
            //ViewBag.LichPhongVans = new SelectList(db.LichPhongVans, "maCV", "maCV");
            //ViewBag.TrangThaiCVs = new SelectList(db.TrangThaiCVs, "maTT", "tenTT", 2);
            //return PartialView("PVCreate", congViec);
        }

        // GET: CongViec/Edit/5
        public ActionResult PVEdit(int? id)
        {
            logger.Info("HttpGet recived. Contoller: CongViecController, ActionResult: PVEdit.");
            if (id == null)
            {
                logger.Info("/tId is null.");
                IList<Notify> notificationList = new List<Notify>();
                notificationList.Add(new Notify("Mã công việc rỗng.", NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyCongViec");
            }
            CongViec congViec = congViecService.XemThongTinCV(id ?? default(int));
            if (congViec == null)
            {
                logger.Info("/tId not found.");
                IList<Notify> notificationList = new List<Notify>();
                notificationList.Add(new Notify(String.Format("Công việc không tồn tại hoặc đã xóa. Mã công việc {0}.", congViec.maCV), NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyCongViec");
            }


            logger.Info("/tReturn partial view PVEdit.");
            return PartialView("PVEdit", congViec);
        }

        // POST: CongViec/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PVEdit([Bind(Include = "maCV,tenCV,yeuCauCV,moTaCV,hanHoanThanh")] CongViec congViec)
        {
            logger.Info("HttpPost recived. Contoller: CongViecController, ActionResult: PVEdit." +
                " maCV = " + congViec.maCV +
                " tenCV = " + congViec.tenCV +
                " yeuCauCV = " + congViec.yeuCauCV +
                " moTaCV = " + congViec.moTaCV +
                " hanHoanThanh = " + congViec.hanHoanThanh
                );
            IList<Notify> notificationList = new List<Notify>();
            if (ModelState.IsValid)
            {
                int status = congViecService.CapNhatThongTinCV(congViec);

                if (status == 0)
                {
                    logger.Info("/t.Update successfully.");
                    //ViewBag.msgType = "sussces";
                    //ViewBag.msg = "Cập nhật công việc thành công.";
                    notificationList.Add(new Notify(String.Format("Cập nhật công việc thành công. Mã công việc {0}.", congViec.maCV), NotificationType.SUCCESS));
                }
                else if (status < 0)
                {
                    logger.Info("/t.Update unsuccessfully.");
                    //ViewBag.msgType = "warning"; ViewBag.msgTitle = "Lỗi!";
                    //ViewBag.msg = "Cập nhật công việc không thành công.";
                    notificationList.Add(new Notify(String.Format("Cập nhật công việc không thành công. Mã công việc {0}.", congViec.maCV), NotificationType.ERROR));
                }
                else if (status > 0)
                {
                    logger.Info("/t.Updated with warning.");
                    //ViewBag.msgType = "warning"; ViewBag.msgTitle = "Cảnh báo!";
                    //ViewBag.msg = "Công việc đã được cập nhật.";
                    notificationList.Add(new Notify(String.Format("Công việc đã được cập nhật. Mã công việc {0}.", congViec.maCV), NotificationType.WARNING));
                }

                logger.Info("/tReturn QuanLyCongViec ActionResult.");
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyCongViec");
            }
            //ViewBag.LichPhongVans = new SelectList(lichPhongVanService.XemThongTinLPV(), "maCV", "maCV");
            //ViewBag.TrangThaiCVs = new SelectList(trangThaiCVService.XemTrangThaiCV(), "maTT", "tenTT");
            notificationList.Add(new Notify("Thông tin công việc không hợp lệ.", NotificationType.ERROR));
            TempData["notificationList"] = notificationList;
            return RedirectToAction("QuanLyCongViec");
        }

        // GET: CongViec/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    logger.Info("HttpGet recived. Contoller: CongViecController, ActionResult: PVEdit." +
        //   if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CongViec congViec = congViecService.XemThongTinCV(id);
        //    if (congViec == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(congViec);
        //}

        // POST: CongViec/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //CongViec congViec = db.CongViecs.Find(id);
            //db.CongViecs.Remove(congViec);
            //db.SaveChanges();
            var view = QuanLyCongViec();

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
