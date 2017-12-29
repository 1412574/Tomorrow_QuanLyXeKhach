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
    public class HanhTrinhsController : Controller
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        IHanhTrinhService<HanhTrinh> hanhTrinhService;
        ITuyenXeService<TuyenXe> tuyenXeService;
        ITramXeService<TramXe> tramXeService;

        public HanhTrinhsController(ITuyenXeService<TuyenXe> tuyenXeService, ITramXeService<TramXe> tramXeService, IHanhTrinhService<HanhTrinh> hanhTrinhService)
        {
            this.hanhTrinhService = hanhTrinhService;
            this.tuyenXeService = tuyenXeService;
            this.tramXeService = tramXeService;
        }

        // GET: HanhTrinhs
        public ActionResult Index()
        {
            var hanhTrinhs = hanhTrinhService.XemHanhTrinh();
            return View(hanhTrinhs);
        }

        // GET: HanhTrinhs/Details/5
        public ActionResult Details(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            HanhTrinh hanhTrinh = hanhTrinhService.LayHanhTrinh(id);
            if (hanhTrinh == null)
            {
                return HttpNotFound();
            }
            return View(hanhTrinh);
        }

        // GET: ChuyenXes/Create
        public ActionResult Create()
        {
            ViewBag.MaTuyenXe = new SelectList(tuyenXeService.XemTuyenXe(), "MaTuyenXe", "TenTuyenXe");
            ViewBag.MaTramXe = new SelectList(tramXeService.XemTramXe(), "MaTramXe", "TenTramXe");
            return View();
        }

        // POST: HanhTrinhs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHanhTrinh,MaTuyenXe,MaTramXe,ThuTu")] HanhTrinh hanhTrinh)
        {
            logger.Info("Start controller....");
            int status = hanhTrinhService.ThemHanhTrinh(hanhTrinh);
            if (status == 0)
            {
                logger.Info("Status: Success");
                ViewBag.MaTuyenXe = new SelectList(tuyenXeService.XemTuyenXe(), "MaTuyenXe", "TenTuyenXe");
                ViewBag.MaTramXe = new SelectList(tramXeService.XemTramXe(), "MaTramXe", "TenTramXe");
                return RedirectToAction("Index");
            }
            else
            {
                logger.Info("Status: Fail");
                return Content("Them that bai");
            }
        }

        // GET: HanhTrinhs/Edit/5
        public ActionResult Edit(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            HanhTrinh hanhTrinh = hanhTrinhService.LayHanhTrinh(id);
            if (hanhTrinh == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaTuyenXe = new SelectList(tuyenXeService.XemTuyenXe(), "MaTuyenXe", "TenTuyenXe");
            ViewBag.MaTramXe = new SelectList(tramXeService.XemTramXe(), "MaTramXe", "TenTramXe");
            return View(hanhTrinh);
        }

        // POST: HanhTrinhs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHanhTrinh,MaTuyenXe,MaTramXe,ThuTu")] HanhTrinh hanhTrinh)
        {
            if (ModelState.IsValid)
            {
                hanhTrinhService.CapNhatHanhTrinh(hanhTrinh);

                return RedirectToAction("Index");
            }
            ViewBag.MaTuyenXe = new SelectList(tuyenXeService.XemTuyenXe(), "MaTuyenXe", "TenTuyenXe", hanhTrinh.MaTuyenXe);
            ViewBag.MaTramXe = new SelectList(tramXeService.XemTramXe(), "MaTramXe", "TenTramXe", hanhTrinh.MaTramXe);
            return View(hanhTrinh);
        }

        // GET: HanhTrinhs/Delete/5
        public ActionResult Delete(int id)
        {
            //    if (id == null)
            //    {
            //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //    }
            HanhTrinh hanhTrinh = hanhTrinhService.LayHanhTrinh(id);
            if (hanhTrinh == null)
            {
                return HttpNotFound();
            }
            return View(hanhTrinh);
        }

        // POST: HanhTrinhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            hanhTrinhService.XoaHanhTrinh(id);
            return RedirectToAction("Index");
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
