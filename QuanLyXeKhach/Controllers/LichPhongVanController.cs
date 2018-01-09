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
    public class LichPhongVanController : Controller
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        //ILichPhongVanService<LichPhongVan> lichPhongVanService;
        //ITrangThaiUVService<TrangThaiUV> trangThaiUVService;
        ILichPhongVanService<LichPhongVan> lichPhongVanService;

        public LichPhongVanController(ILichPhongVanService<LichPhongVan> _lichPhongVanService)
        {
            //lichPhongVanService = _lichPhongVanService;
            //trangThaiUVService = _trangThaiUVService;
            lichPhongVanService = _lichPhongVanService;
        }


        public ActionResult QuanLyLichPhongVan(string filter = null)
        {
            logger.Info("HttpGet recived. Contoller: LichPhongVanController, ActionResult: QuanLyLPV.");
            //ViewBag.LichPhongVans = new SelectList(lichPhongVanService.XemLichPhongVan(), "maLPV", "maLPV");
            //ViewBag.TrangThaiUVs = new SelectList(trangThaiUVService.XemTrangThaiUV(), "maTT", "tenTT");
            //var lichPhongVans = lichPhongVanService.XemThongTinUV();

            //foreach (var ungvien in lichPhongVans)
            //{
            //    if (ungvien.maLPV == null)
            //        ungvien.LichPhongVan = null;
            //    else
            //        ungvien.LichPhongVan = lichPhongVanService.XemLichPhongVan(ungvien.maLPV ?? default(int));
            //    ungvien.TrangThaiUV = trangThaiUVService.XemTrangThaiUV(ungvien.trangThai);
            //}
            //logger.Info("/Return to action QuanLyLichPhongVan with list of LichPhongVan as model.");
            //return View(lichPhongVans.ToList());
            var records = new List<LichPhongVan>();
            var notificationList = TempData["notificationList"] as IList<Notify>;
            ViewBag.filter = filter;
            records = lichPhongVanService.XemThongTinLPV(filter).ToList();

            foreach (var lPV in records)
            {
                
            }
            logger.Info("/Return to action QuanLyLPV with list of LichPhongVan as model.");
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
            logger.Info("HttpGet recived. Contoller: LichPhongVanController, ActionResult: PVConfirmDelete, Id: " + id.ToString() + ".");
            if (id == null)
            {
                logger.Info("/tId is null.");
                IList<Notify> notificationList = new List<Notify>();
                notificationList.Add(new Notify("Mã lịch phỏng vấn rỗng.", NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyLichPhongVan");
            }
            LichPhongVan lichPhongVan = lichPhongVanService.XemThongTinLPV(id ?? default(int));
            if (lichPhongVan == null)
            {
                logger.Info("/tId not found.");
                IList<Notify> notificationList = new List<Notify>();
                notificationList.Add(new Notify(String.Format("Lịch phỏng vấn không tồn tại hoặc đã xóa. Mã lịch phỏng vấn {0}.", lichPhongVan.maLPV), NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyLichPhongVan");
            }

            logger.Info("/tReturn partial view PVConfirmDelete.");
            return PartialView("PVConfirmDelete", lichPhongVan);
        }

        [HttpPost, ActionName("PVConfirmDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult PVConfirmDelete(int id)
        {
            logger.Info("HttpPost recived. Contoller: LichPhongVanController, ActionResult: PVConfirmDelete, Id: " + id.ToString() + ".");
            LichPhongVan lichPhongVan = lichPhongVanService.XemThongTinLPV(id);
            IList<Notify> notificationList = new List<Notify>();
            if (lichPhongVan == null)
            {
                logger.Info("/tId not found.");
                notificationList.Add(new Notify(String.Format("Lịch phỏng vấn không tồn tại hoặc đã xóa. Mã lịch phỏng vấn {0}.", lichPhongVan.maLPV), NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyLichPhongVan");
            }
            int status = lichPhongVanService.XoaLichPhongVan(id);
            if (status == 0)
            {
                logger.Info("/t.Delete successfully.");
                //ViewBag.msgType = "sussces";
                //ViewBag.msg = "Xóa lịch phỏng vấn thành công.";
                notificationList.Add(new Notify(String.Format("Xóa lịch phỏng vấn thành công. Mã lịch phỏng vấn {0}.", lichPhongVan.maLPV), NotificationType.SUCCESS));
            }
            else if (status < 0)
            {
                logger.Info("/t.Delete unsuccessfully.");
                //ViewBag.msgType = "warning"; ViewBag.msgTitle = "Lỗi!";
                //ViewBag.msg = "Xóa lịch phỏng vấn thất bại.";
                notificationList.Add(new Notify(String.Format("Xóa lịch phỏng vấn thất bại. Mã lịch phỏng vấn {0}.", lichPhongVan.maLPV), NotificationType.ERROR));
            }
            else if (status > 0)
            {
                logger.Info("/t.Deleted with warning.");
                //ViewBag.msgType = "warning"; ViewBag.msgTitle = "Cảnh báo!";
                //ViewBag.msg = "Lịch phỏng vấn đã được xóa.";
                notificationList.Add(new Notify(String.Format("Lịch phỏng vấn đã được xóa. Mã lịch phỏng vấn {0}.", lichPhongVan.maLPV), NotificationType.WARNING));
            }

            logger.Info("/tRedirect to action QuanLyLichPhongVan.");
            TempData["notificationList"] = notificationList;
            return RedirectToAction("QuanLyLichPhongVan");
        }

        //// GET: LichPhongVan
        //public ActionResult Index()
        //{
        //    var lichPhongVan = db.LichPhongVans.Include(u => u.LichPhongVan).Include(t => t.TrangThaiUV);
        //    return View(lichPhongVan.ToList());
        //}

        // GET: LichPhongVan/Details/5
        public ActionResult PVDetails(int? id)
        {
            logger.Info("HttpGet recived. Contoller: LichPhongVanController, ActionResult: PVDetails, Id: " + id.ToString() + ".");
            if (id == null)
            {
                logger.Info("/tId is null.");
                IList<Notify> notificationList = new List<Notify>();
                notificationList.Add(new Notify("Mã lịch phỏng vấn rỗng.", NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyLichPhongVan");
            }

            LichPhongVan lichPhongVan = lichPhongVanService.XemThongTinLPV(id ?? default(int));
            if (lichPhongVan == null)
            {
                logger.Info("/tId not found.");
                IList<Notify> notificationList = new List<Notify>();
                notificationList.Add(new Notify(String.Format("Lịch phỏng vấn không tồn tại hoặc đã xóa. Mã lịch phỏng vấn {0}.", lichPhongVan.maLPV), NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyLichPhongVan");
            }
            logger.Info("/tReturn partial view PVDetails.");
            return PartialView("PVDetails", lichPhongVan);
        }

        // GET: LichPhongVan/Create
        public ActionResult PVCreate()
        {
            logger.Info("HttpGet recived. Contoller: LichPhongVanController, ActionResult: PVCreate.");


            logger.Info("/tReturn partial view PVCreate.");
            return PartialView("PVCreate");
        }

        // GET: LichPhongVan/Create
        //public ActionResult PVCreate(LichPhongVan lichPhongVan)
        //{
        //    ViewBag.LichPhongVans = new SelectList(db.LichPhongVans, "maLPV", "maLPV");
        //    ViewBag.TrangThaiUVs = new SelectList(db.TrangThaiUVs, "maTT", "tenTT");
        //    return PartialView("PVCreate", lichPhongVan);
        //}

        // POST: LichPhongVan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PVCreate([Bind(Include = "maLPV,ngay,diaDiem,tieuChi,ghiChu")] LichPhongVan lichPhongVan)
        {
            logger.Info("HttpPost recived. Contoller: LichPhongVanController, ActionResult: PVCreate." +
                " ngay = " + lichPhongVan.ngay +
                " diaDiem = " + lichPhongVan.diaDiem +
                " tieuChi = " + lichPhongVan.tieuChi +
                " ghiChu = " + lichPhongVan.ghiChu 
                );
            //lichPhongVan.TrangThaiUV = trangThaiUVService.XemTrangThaiUV(lichPhongVan.trangThai);
            //if (lichPhongVan.maLPV == null)
            //    lichPhongVan.LichPhongVan = null;
            //else
            //    lichPhongVan.LichPhongVan = lichPhongVanService.XemLichPhongVan(lichPhongVan.maLPV ?? default(int));

            //lichPhongVan.maUV = 0;

            int status = lichPhongVanService.ThemLichPhongVan(lichPhongVan);
            IList<Notify> notificationList = new List<Notify>();
            if (status == 0)
            {
                logger.Info("/t.Create successfully.");
                //ViewBag.msgType = "sussces";
                //ViewBag.msg = "Thêm lịch phỏng vấn thành công.";
                notificationList.Add(new Notify(String.Format("Thêm lịch phỏng vấn thành công. Mã lịch phỏng vấn {0}.", lichPhongVan.maLPV), NotificationType.SUCCESS));
            }
            else if (status < 0)
            {
                logger.Info("/t.Create unsuccessfully.");
                //ViewBag.msgType = "warning"; ViewBag.msgTitle = "Lỗi!";
                //ViewBag.msg = "Thêm lịch phỏng vấn thành công.";
                notificationList.Add(new Notify("Thêm lịch phỏng vấn không thành công.", NotificationType.ERROR));
            }
            else if (status > 0)
            {
                logger.Info("/t.Deleted with warning.");
                //ViewBag.msgType = "warning"; ViewBag.msgTitle = "Cảnh báo!";
                //ViewBag.msg = "Lịch phỏng vấn đã được thêm.";
                notificationList.Add(new Notify(String.Format("Lịch phỏng vấn đã được thêm. Mã lịch phỏng vấn {0}.", lichPhongVan.maLPV), NotificationType.WARNING));
            }
            logger.Info("/tRedirect to action QuanLyLichPhongVan.");
            TempData["notificationList"] = notificationList;
            return RedirectToAction("QuanLyLichPhongVan");

            //if (ModelState.IsValid)
            //{
            //    db.LichPhongVans.Add(lichPhongVan);
            //    db.SaveChanges();
            //    return RedirectToAction("QuanLyLichPhongVan");
            //}
            //ViewBag.LichPhongVans = new SelectList(db.LichPhongVans, "maLPV", "maLPV");
            //ViewBag.TrangThaiUVs = new SelectList(db.TrangThaiUVs, "maTT", "tenTT", 2);
            //return PartialView("PVCreate", lichPhongVan);
        }

        // GET: LichPhongVan/Edit/5
        public ActionResult PVEdit(int? id)
        {
            logger.Info("HttpGet recived. Contoller: LichPhongVanController, ActionResult: PVEdit.");
            if (id == null)
            {
                logger.Info("/tId is null.");
                IList<Notify> notificationList = new List<Notify>();
                notificationList.Add(new Notify("Mã lịch phỏng vấn rỗng.", NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyLichPhongVan");
            }
            LichPhongVan lichPhongVan = lichPhongVanService.XemThongTinLPV(id ?? default(int));
            if (lichPhongVan == null)
            {
                logger.Info("/tId not found.");
                IList<Notify> notificationList = new List<Notify>();
                notificationList.Add(new Notify(String.Format("Lịch phỏng vấn không tồn tại hoặc đã xóa. Mã lịch phỏng vấn {0}.", lichPhongVan.maLPV), NotificationType.ERROR));
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyLichPhongVan");
            }



            logger.Info("/tReturn partial view PVEdit.");
            return PartialView("PVEdit", lichPhongVan);
        }

        // POST: LichPhongVan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PVEdit([Bind(Include = "maLPV,ngay,diaDiem,tieuChi,ghiChu")] LichPhongVan lichPhongVan)
        {
            logger.Info("HttpPost recived. Contoller: LichPhongVanController, ActionResult: PVEdit." +
                " ngay = " + lichPhongVan.ngay +
                " diaDiem = " + lichPhongVan.diaDiem +
                " tieuChi = " + lichPhongVan.tieuChi +
                " ghiChu = " + lichPhongVan.ghiChu
                );
            IList<Notify> notificationList = new List<Notify>();
            if (ModelState.IsValid)
            {
                int status = lichPhongVanService.CapNhatThongTinLPV(lichPhongVan);

                if (status == 0)
                {
                    logger.Info("/t.Update successfully.");
                    //ViewBag.msgType = "sussces";
                    //ViewBag.msg = "Cập nhật lịch phỏng vấn thành công.";
                    notificationList.Add(new Notify(String.Format("Cập nhật lịch phỏng vấn thành công. Mã lịch phỏng vấn {0}.", lichPhongVan.maLPV), NotificationType.SUCCESS));
                }
                else if (status < 0)
                {
                    logger.Info("/t.Update unsuccessfully.");
                    //ViewBag.msgType = "warning"; ViewBag.msgTitle = "Lỗi!";
                    //ViewBag.msg = "Cập nhật lịch phỏng vấn không thành công.";
                    notificationList.Add(new Notify(String.Format("Cập nhật lịch phỏng vấn không thành công. Mã lịch phỏng vấn {0}.", lichPhongVan.maLPV), NotificationType.ERROR));
                }
                else if (status > 0)
                {
                    logger.Info("/t.Updated with warning.");
                    //ViewBag.msgType = "warning"; ViewBag.msgTitle = "Cảnh báo!";
                    //ViewBag.msg = "Lịch phỏng vấn đã được cập nhật .";
                    notificationList.Add(new Notify(String.Format("Lịch phỏng vấn đã được cập nhật. Mã lịch phỏng vấn {0}.", lichPhongVan.maLPV), NotificationType.WARNING));
                }

                //return this.ModalDialog(RetResult);
                logger.Info("/tRedirect to action QuanLyLichPhongVan.");
                TempData["notificationList"] = notificationList;
                return RedirectToAction("QuanLyLichPhongVan");
            }
            notificationList.Add(new Notify("Thông tin lịch phỏng vấn không hợp lệ.", NotificationType.ERROR));
            TempData["notificationList"] = notificationList;
            return RedirectToAction("QuanLyLichPhongVan");
        }

        // GET: LichPhongVan/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    logger.Info("HttpGet recived. Contoller: LichPhongVanController, ActionResult: PVEdit." +
        //   if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    LichPhongVan lichPhongVan = lichPhongVanService.XemThongTinUV(id);
        //    if (lichPhongVan == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(lichPhongVan);
        //}

        // POST: LichPhongVan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //LichPhongVan lichPhongVan = db.LichPhongVans.Find(id);
            //db.LichPhongVans.Remove(lichPhongVan);
            //db.SaveChanges();
            return RedirectToAction("QuanLyLichPhongVan");
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
