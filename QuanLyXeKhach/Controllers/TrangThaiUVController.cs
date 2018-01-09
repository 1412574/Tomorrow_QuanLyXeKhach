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
    public class TrangThaiUVController : Controller
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        //ITrangThaiUVService<TrangThaiUV> trangThaiUVService;
        //ITrangThaiUVService<TrangThaiUV> trangThaiUVService;
        ITrangThaiUVService<TrangThaiUV> trangThaiUVService;

        public TrangThaiUVController(ITrangThaiUVService<TrangThaiUV> _trangThaiUVService)
        {
            //trangThaiUVService = _trangThaiUVService;
            //trangThaiUVService = _trangThaiUVService;
            trangThaiUVService = _trangThaiUVService;
        }


        public ActionResult QuanLyTrangThaiUV(string filter = null)
        {
            logger.Info("HttpGet recived. Contoller: TrangThaiUVController, ActionResult: QuanLyTTUV.");
            var records = new List<TrangThaiUV>();
            var notificationList = TempData["notificationList"] as IList<Notify>;
            ViewBag.filter = filter;
            records = trangThaiUVService.XemTrangThaiUV(filter).ToList();

            foreach (var lPV in records)
            {

            }
            logger.Info("/Return to action QuanLyTTUV with list of TrangThaiUV as model.");
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
            logger.Info("HttpGet recived. Contoller: TrangThaiUVController, ActionResult: PVConfirmDelete, Id: " + id.ToString() + ".");
            if (id == null)
            {
                logger.Info("/tId is null.");
                IList<Notify> notificationList = new List<Notify>();
                notificationList.Add(new Notify("Mã trạng thái ứng viên rỗng.", NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyTrangThaiUV");
            }
            TrangThaiUV trangThaiUngVien = trangThaiUVService.XemTrangThaiUV(id ?? default(int));
            if (trangThaiUngVien == null)
            {
                logger.Info("/tId not found.");
                IList<Notify> notificationList = new List<Notify>();
                notificationList.Add(new Notify(String.Format("Trạng thái ứng viên không tồn tại hoặc đã xóa. Mã trạng thái ứng viên {0}.", trangThaiUngVien.maTT), NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyTrangThaiUV");
            }

            logger.Info("/tReturn partial view PVConfirmDelete.");
            return PartialView("PVConfirmDelete", trangThaiUngVien);
        }

        [HttpPost, ActionName("PVConfirmDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult PVConfirmDelete(int id)
        {
            logger.Info("HttpPost recived. Contoller: TrangThaiUVController, ActionResult: PVConfirmDelete, Id: " + id.ToString() + ".");
            TrangThaiUV trangThaiUngVien = trangThaiUVService.XemTrangThaiUV(id);
            IList<Notify> notificationList = new List<Notify>();
            if (trangThaiUngVien == null)
            {
                logger.Info("/tId not found.");
                notificationList.Add(new Notify(String.Format("Trạng thái ứng viên không tồn tại hoặc đã xóa. Mã trạng thái ứng viên {0}.", trangThaiUngVien.maTT), NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyTrangThaiUV");
            }
            int status = trangThaiUVService.XoaTrangThaiUV(id);
            if (status == 0)
            {
                logger.Info("/t.Delete successfully.");
                //ViewBag.msgType = "sussces";
                //ViewBag.msg = "Xóa trạng thái ứng viên thành công.";
                notificationList.Add(new Notify(String.Format("Xóa trạng thái ứng viên thành công. Mã trạng thái ứng viên {0}.", trangThaiUngVien.maTT), NotificationType.SUCCESS));
            }
            else if (status < 0)
            {
                logger.Info("/t.Delete unsuccessfully.");
                //ViewBag.msgType = "warning"; ViewBag.msgTitle = "Lỗi!";
                //ViewBag.msg = "Xóa trạng thái ứng viên thất bại.";
                notificationList.Add(new Notify(String.Format("Xóa trạng thái ứng viên thất bại. Mã trạng thái ứng viên {0}.", trangThaiUngVien.maTT), NotificationType.ERROR));
            }
            else if (status > 0)
            {
                logger.Info("/t.Deleted with warning.");
                //ViewBag.msgType = "warning"; ViewBag.msgTitle = "Cảnh báo!";
                //ViewBag.msg = "Trạng thái ứng viên đã được xóa.";
                notificationList.Add(new Notify(String.Format("Trạng thái ứng viên đã được xóa. Mã trạng thái ứng viên {0}.", trangThaiUngVien.maTT), NotificationType.WARNING));
            }

            logger.Info("/tRedirect to action QuanLyTrangThaiUV.");
            TempData["notificationList"] = notificationList;
            return RedirectToAction("QuanLyTrangThaiUV");
        }

        //// GET: TrangThaiUV
        //public ActionResult Index()
        //{
        //    var trangThaiUngVien = db.TrangThaiUVs.Include(u => u.TrangThaiUV).Include(t => t.TrangThaiUV);
        //    return View(trangThaiUngVien.ToList());
        //}

        // GET: TrangThaiUV/Details/5
        public ActionResult PVDetails(int? id)
        {
            logger.Info("HttpGet recived. Contoller: TrangThaiUVController, ActionResult: PVDetails, Id: " + id.ToString() + ".");
            if (id == null)
            {
                logger.Info("/tId is null.");
                IList<Notify> notificationList = new List<Notify>();
                notificationList.Add(new Notify("Mã trạng thái ứng viên rỗng.", NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyTrangThaiUV");
            }

            TrangThaiUV trangThaiUngVien = trangThaiUVService.XemTrangThaiUV(id ?? default(int));
            if (trangThaiUngVien == null)
            {
                logger.Info("/tId not found.");
                IList<Notify> notificationList = new List<Notify>();
                notificationList.Add(new Notify(String.Format("Trạng thái ứng viên không tồn tại hoặc đã xóa. Mã trạng thái ứng viên {0}.", trangThaiUngVien.maTT), NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyTrangThaiUV");
            }
            logger.Info("/tReturn partial view PVDetails.");
            return PartialView("PVDetails", trangThaiUngVien);
        }

        // GET: TrangThaiUV/Create
        public ActionResult PVCreate()
        {
            logger.Info("HttpGet recived. Contoller: TrangThaiUVController, ActionResult: PVCreate.");


            logger.Info("/tReturn partial view PVCreate.");
            return PartialView("PVCreate");
        }

        // GET: TrangThaiUV/Create
        //public ActionResult PVCreate(TrangThaiUV trangThaiUngVien)
        //{
        //    ViewBag.TrangThaiUVs = new SelectList(db.TrangThaiUVs, "maTT", "maTT");
        //    ViewBag.TrangThaiUVs = new SelectList(db.TrangThaiUVs, "maTT", "tenTT");
        //    return PartialView("PVCreate", trangThaiUngVien);
        //}

        // POST: TrangThaiUV/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PVCreate([Bind(Include = "maTT,tenTT,moTaTT")] TrangThaiUV trangThaiUngVien)
        {
            logger.Info("HttpPost recived. Contoller: TrangThaiUVController, ActionResult: PVCreate." +
                " tenTT = " + trangThaiUngVien.tenTT +
                " moTaTT = " + trangThaiUngVien.moTaTT
                );
            //trangThaiUngVien.TrangThaiUV = trangThaiUVService.XemTrangThaiUV(trangThaiUngVien.trangThai);
            //if (trangThaiUngVien.maTT == null)
            //    trangThaiUngVien.TrangThaiUV = null;
            //else
            //    trangThaiUngVien.TrangThaiUV = trangThaiUVService.XemTrangThaiUV(trangThaiUngVien.maTT ?? default(int));

            //trangThaiUngVien.maUV = 0;

            int status = trangThaiUVService.ThemTrangThaiUV(trangThaiUngVien);
            IList<Notify> notificationList = new List<Notify>();
            if (status == 0)
            {
                logger.Info("/t.Create successfully.");
                //ViewBag.msgType = "sussces";
                //ViewBag.msg = "Thêm trạng thái ứng viên thành công.";
                notificationList.Add(new Notify(String.Format("Thêm trạng thái ứng viên thành công. Mã trạng thái ứng viên {0}.", trangThaiUngVien.maTT), NotificationType.SUCCESS));
            }
            else if (status < 0)
            {
                logger.Info("/t.Create unsuccessfully.");
                //ViewBag.msgType = "warning"; ViewBag.msgTitle = "Lỗi!";
                //ViewBag.msg = "Thêm trạng thái ứng viên thành công.";
                notificationList.Add(new Notify("Thêm trạng thái ứng viên không thành công.", NotificationType.ERROR));
            }
            else if (status > 0)
            {
                logger.Info("/t.Deleted with warning.");
                //ViewBag.msgType = "warning"; ViewBag.msgTitle = "Cảnh báo!";
                //ViewBag.msg = "Trạng thái ứng viên đã được thêm.";
                notificationList.Add(new Notify(String.Format("Trạng thái ứng viên đã được thêm. Mã trạng thái ứng viên {0}.", trangThaiUngVien.maTT), NotificationType.WARNING));
            }
            logger.Info("/tRedirect to action QuanLyTrangThaiUV.");
            TempData["notificationList"] = notificationList;
            return RedirectToAction("QuanLyTrangThaiUV");

            //if (ModelState.IsValid)
            //{
            //    db.TrangThaiUVs.Add(trangThaiUngVien);
            //    db.SaveChanges();
            //    return RedirectToAction("QuanLyTrangThaiUV");
            //}
            //ViewBag.TrangThaiUVs = new SelectList(db.TrangThaiUVs, "maTT", "maTT");
            //ViewBag.TrangThaiUVs = new SelectList(db.TrangThaiUVs, "maTT", "tenTT", 2);
            //return PartialView("PVCreate", trangThaiUngVien);
        }

        // GET: TrangThaiUV/Edit/5
        public ActionResult PVEdit(int? id)
        {
            logger.Info("HttpGet recived. Contoller: TrangThaiUVController, ActionResult: PVEdit.");
            if (id == null)
            {
                logger.Info("/tId is null.");
                IList<Notify> notificationList = new List<Notify>();
                notificationList.Add(new Notify("Mã trạng thái ứng viên rỗng.", NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyTrangThaiUV");
            }
            TrangThaiUV trangThaiUngVien = trangThaiUVService.XemTrangThaiUV(id ?? default(int));
            if (trangThaiUngVien == null)
            {
                logger.Info("/tId not found.");
                IList<Notify> notificationList = new List<Notify>();
                notificationList.Add(new Notify(String.Format("Trạng thái ứng viên không tồn tại hoặc đã xóa. Mã trạng thái ứng viên {0}.", trangThaiUngVien.maTT), NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyTrangThaiUV");
            }



            logger.Info("/tReturn partial view PVEdit.");
            return PartialView("PVEdit", trangThaiUngVien);
        }

        // POST: TrangThaiUV/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PVEdit([Bind(Include = "maTT,tenTT,moTaTT")] TrangThaiUV trangThaiUngVien)
        {
            logger.Info("HttpPost recived. Contoller: TrangThaiUVController, ActionResult: PVEdit." +
                " tenTT = " + trangThaiUngVien.tenTT +
                " moTaTT = " + trangThaiUngVien.moTaTT
                );
            IList<Notify> notificationList = new List<Notify>();
            if (ModelState.IsValid)
            {
                int status = trangThaiUVService.CapNhatTrangThaiUV(trangThaiUngVien);

                if (status == 0)
                {
                    logger.Info("/t.Update successfully.");
                    //ViewBag.msgType = "sussces";
                    //ViewBag.msg = "Cập nhật trạng thái ứng viên thành công.";
                    notificationList.Add(new Notify(String.Format("Cập nhật trạng thái ứng viên thành công. Mã trạng thái ứng viên {0}.", trangThaiUngVien.maTT), NotificationType.SUCCESS));
                }
                else if (status < 0)
                {
                    logger.Info("/t.Update unsuccessfully.");
                    //ViewBag.msgType = "warning"; ViewBag.msgTitle = "Lỗi!";
                    //ViewBag.msg = "Cập nhật trạng thái ứng viên không thành công.";
                    notificationList.Add(new Notify(String.Format("Cập nhật trạng thái ứng viên không thành công. Mã trạng thái ứng viên {0}.", trangThaiUngVien.maTT), NotificationType.ERROR));
                }
                else if (status > 0)
                {
                    logger.Info("/t.Updated with warning.");
                    //ViewBag.msgType = "warning"; ViewBag.msgTitle = "Cảnh báo!";
                    //ViewBag.msg = "Trạng thái ứng viên đã được cập nhật .";
                    notificationList.Add(new Notify(String.Format("Trạng thái ứng viên đã được cập nhật. Mã trạng thái ứng viên {0}.", trangThaiUngVien.maTT), NotificationType.WARNING));
                }

                //return this.ModalDialog(RetResult);
                logger.Info("/tRedirect to action QuanLyTrangThaiUV.");
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyTrangThaiUV");
            }
            notificationList.Add(new Notify("Thông tin trạng thái ứng viên không hợp lệ.", NotificationType.ERROR));
            TempData["notificationList"] = notificationList;
            return RedirectToAction("QuanLyTrangThaiUV");
        }

        // GET: TrangThaiUV/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    logger.Info("HttpGet recived. Contoller: TrangThaiUVController, ActionResult: PVEdit." +
        //   if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TrangThaiUV trangThaiUngVien = trangThaiUVService.XemThongTinUV(id);
        //    if (trangThaiUngVien == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(trangThaiUngVien);
        //}

        // POST: TrangThaiUV/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //TrangThaiUV trangThaiUngVien = db.TrangThaiUVs.Find(id);
            //db.TrangThaiUVs.Remove(trangThaiUngVien);
            //db.SaveChanges();
            return RedirectToAction("QuanLyTrangThaiUV");
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
