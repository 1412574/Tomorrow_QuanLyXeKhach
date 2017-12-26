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
using System.Collections;

namespace QuanLyXeKhach.Controllers
{
    public class TuyenXeController : Controller
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        ITuyenXeService<TuyenXe> tuyenXeService;
        ITramXeService<TramXe> tramXeService;

        public TuyenXeController(ITuyenXeService<TuyenXe> tuyenXeService, ITramXeService<TramXe> tramXeService)
        {
            this.tuyenXeService = tuyenXeService;
            this.tramXeService = tramXeService;
        }

        // GET: TuyenXes
        public ActionResult Index()
        {
            var tuyenXes = tuyenXeService.XemTuyenXe();
            return View(tuyenXes);
        }

        // GET: TuyenXes/Details/5
        public ActionResult Details(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            TuyenXe tuyenXe = tuyenXeService.LayTuyenXe(id);
            if (tuyenXe == null)
            {
                return HttpNotFound();
            }

            ViewBag.HanhTrinh = tramXeService.XemHanhTrinhTheoMaTuyen(id);

            return View(tuyenXe);
        }

        // GET: TuyenXes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TuyenXes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTuyenXe,TenTuyenXe,GiaVe,LoaiXe,ThoiGian,QuangDuong,SoChuyen")] TuyenXe tuyenXe)
        {
            logger.Info("Start controller....");
            int status = tuyenXeService.ThemTuyenXe(tuyenXe);
            if (status == 0)
            {
                logger.Info("Status: Success");
                return RedirectToAction("Index");
            }
            else
            {
                logger.Info("Status: Fail");
                return Content("Them that bai");
            }
        }

        // GET: TuyenXes/Edit/5
        public ActionResult Edit(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            TuyenXe tuyenXe = tuyenXeService.LayTuyenXe(id);
            if (tuyenXe == null)
            {
                return HttpNotFound();
            }
            return View(tuyenXe);
        }

        // POST: TuyenXes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTuyenXe,TenTuyenXe,GiaVe,LoaiXe,ThoiGian,QuangDuong,SoChuyen")] TuyenXe tuyenXe)
        {
            if (ModelState.IsValid)
            {
                tuyenXeService.CapNhatTuyenXe(tuyenXe);

                return RedirectToAction("Index");
            }
            return View(tuyenXe);
        }

        // GET: TuyenXes/Delete/5
        public ActionResult Delete(int id)
        {
            //    if (id == null)
            //    {
            //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //    }
            TuyenXe tuyenXe = tuyenXeService.LayTuyenXe(id);
            if (tuyenXe == null)
            {
                return HttpNotFound();
            }
            return View(tuyenXe);
        }

        // POST: TuyenXes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tuyenXeService.XoaTuyenXe(id);
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
