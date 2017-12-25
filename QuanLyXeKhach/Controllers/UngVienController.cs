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
         int pageSize = 5, string sort = "PhoneId", string sortdir = "DESC")
        {
            //logger.Info("HttpGet recived. Contoller: UngVienController, ActionResult: QuanLyUngVien.");
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
            ViewBag.filter = filter;
            records.Content = ungVienService.XemThongTinUV(filter).ToList();

            // Count
            records.TotalRecords = records.Content.Count();

            records.CurrentPage = page;
            records.PageSize = pageSize;

            foreach (var ungvien in records.Content)
            {
                if (ungvien.maLPV == null)
                    ungvien.LichPhongVan = null;
                else
                    ungvien.LichPhongVan = lichPhongVanService.XemLichPhongVan(ungvien.maLPV ?? default(int));
                ungvien.TrangThaiUV = trangThaiUVService.XemTrangThaiUV(ungvien.trangThai);
            }
            logger.Info("/Return to action QuanLyUngVien with list of UngVien as model.");
            return View(records);
        }
        public ActionResult PVConfirmDelete(int? id)
        {
            logger.Info("HttpGet recived. Contoller: UngVienController, ActionResult: PVConfirmDelete, Id: " + id.ToString() + ".");
            if (id == null)
            {
                logger.Info("/tId is null.");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UngVien ungVien = ungVienService.XemThongTinUV(id ?? default(int));
            if (ungVien == null)
            {
                logger.Info("/tId not found.");
                return HttpNotFound();
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
            if (ungVien == null)
            {
                logger.Info("/tId not found.");
                return HttpNotFound();
            }
            int status = ungVienService.XoaUngVien(id);
            if (status == 0)
            {
                logger.Info("/t.Delete successfully.");
                ViewBag.msgType = "sussces";
                ViewBag.msg = "Xóa ứng viên thành công.";
            }
            else if (status < 0)
            {
                logger.Info("/t.Delete unsuccessfully.");
                ViewBag.msgType = "warning";ViewBag.msgTitle = "Lỗi!";
                ViewBag.msg = "Xóa ứng viên thất bại.";
            }
            else if (status > 0)
            {
                logger.Info("/t.Deleted with warning.");
                ViewBag.msgType = "warning";ViewBag.msgTitle = "Cảnh báo!";
                ViewBag.msg = "Ứng viên đã được xóa.";
            }

            logger.Info("/tRedirect to action QuanLyUngVien.");
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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UngVien ungVien = ungVienService.XemThongTinUV(id ?? default(int));
            if (ungVien == null)
            {
                logger.Info("/tId not found.");
                return HttpNotFound();
            }
            logger.Info("/tReturn partial view PVDetails.");
            return PartialView("PVDetails", ungVien);
        }

        // GET: UngVien/Create
        public ActionResult PVCreate()
        {
            logger.Info("HttpGet recived. Contoller: UngVienController, ActionResult: PVCreate.");

            ViewBag.LichPhongVans = new SelectList(lichPhongVanService.XemLichPhongVan(), "maLPV", "maLPV");
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
            if (status == 0)
            {
                logger.Info("/t.Create successfully.");
                ViewBag.msgType = "sussces";
                ViewBag.msg = "Thêm ứng viên thành công.";
            }
            else if (status < 0)
            {
                logger.Info("/t.Create unsuccessfully.");
                ViewBag.msgType = "warning";ViewBag.msgTitle = "Lỗi!";
                ViewBag.msg = "Thêm ứng viên thành công.";
            }
            else if (status > 0)
            {
                logger.Info("/t.Deleted with warning.");
                ViewBag.msgType = "warning";ViewBag.msgTitle = "Cảnh báo!";
                ViewBag.msg = "Ứng viên đã được thêm.";
            }
            logger.Info("/tRedirect to action QuanLyUngVien.");
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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UngVien ungVien = ungVienService.XemThongTinUV(id ?? default(int));
            if (ungVien == null)
            {
                logger.Info("/tId not found.");
                return HttpNotFound();
            }


            ViewBag.LichPhongVans = new SelectList(lichPhongVanService.XemLichPhongVan(), "maLPV", "maLPV");
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
            if (ModelState.IsValid)
            {
                //ungVien.TrangThaiUV = trangThaiUVService.XemTrangThaiUV(ungVien.trangThai);
                if (ungVien.maLPV == null)
                {
                    logger.Info("/tmaLPV is null.");
                    //ungVien.LichPhongVan = null;
                }
                else
                {

                    //ungVien.LichPhongVan = lichPhongVanService.XemLichPhongVan(ungVien.maLPV ?? default(int));
                    //if (ungVien.LichPhongVan == null)
                    //{
                    //    logger.Info("/tmaLPV not found. maLPV is set to null.");
                    //}
                }
                int status = ungVienService.CapNhatThongTinUV(ungVien);

                if (status == 0)
                {
                    logger.Info("/t.Update successfully.");
                    ViewBag.msgType = "sussces";
                    ViewBag.msg = "Cập nhật ứng viên thành công.";
                }
                else if (status < 0)
                {
                    logger.Info("/t.Update unsuccessfully.");
                    ViewBag.msgType = "warning";ViewBag.msgTitle = "Lỗi!";
                    ViewBag.msg = "Cập nhật ứng viên không thành công.";
                }
                else if (status > 0)
                {
                    logger.Info("/t.Updated with warning.");
                    ViewBag.msgType = "warning";ViewBag.msgTitle = "Cảnh báo!";
                    ViewBag.msg = "Ứng viên đã được cập nhật .";
                }

                //return this.ModalDialog(RetResult);
                logger.Info("/tRedirect to action QuanLyUngVien.");
                return RedirectToAction("QuanLyUngVien");
            }
            ViewBag.LichPhongVans = new SelectList(lichPhongVanService.XemLichPhongVan(), "maLPV", "maLPV");
            ViewBag.TrangThaiUVs = new SelectList(trangThaiUVService.XemTrangThaiUV(), "maTT", "tenTT");
            return View(ungVien);
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
            return RedirectToAction("QuanLyUngVien");
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
