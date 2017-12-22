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
    public class ChuyenXesController : Controller
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        IChuyenXeService<ChuyenXe> chuyenXeService;
        ITuyenXeService<TuyenXe> tuyenXeService;

        public ChuyenXesController(ITuyenXeService<TuyenXe> tuyenXeService, IChuyenXeService<ChuyenXe> chuyenXeService)
        {
            this.chuyenXeService = chuyenXeService;
            this.tuyenXeService = tuyenXeService;
        }

        // GET: ChuyenXes
        public ActionResult Index()
        {
            var chuyenXes = chuyenXeService.XemChuyenXe();
            return View(chuyenXes);
        }

        // GET: ChuyenXes/Details/5
        public ActionResult Details(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            ChuyenXe chuyenXe = chuyenXeService.LayChuyenXe(id);
            if (chuyenXe == null)
            {
                return HttpNotFound();
            }
            return View(chuyenXe);
        }

        // GET: ChuyenXes/Create
        public ActionResult Create()
        {
            ViewBag.MaTuyenXe = new SelectList(tuyenXeService.XemTuyenXe(), "MaTuyenXe", "TenTuyenXe");
            return View();
        }

        // POST: ChuyenXes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaChuyenXe,MaTuyenXe,TenChuyenXe,NgayGioChay,TaiXe")] ChuyenXe chuyenXe)
        {
            logger.Info("Start controller....");
            int status = chuyenXeService.ThemChuyenXe(chuyenXe);
            if (status == 0)
            {
                logger.Info("Status: Success");
                ViewBag.MaTuyenXe = new SelectList(tuyenXeService.XemTuyenXe(), "MaTuyenXe", "TenTuyenXe");
                return View(chuyenXe);
            }
            else
            {
                logger.Info("Status: Fail");
                return Content("Them that bai");
            }
        }

        // GET: ChuyenXes/Edit/5
        public ActionResult Edit(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            ChuyenXe chuyenXe = chuyenXeService.LayChuyenXe(id);
            if (chuyenXe == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaTuyenXe = new SelectList(tuyenXeService.XemTuyenXe(), "MaTuyenXe", "TenTuyenXe");
            return View(chuyenXe);
        }

        // POST: ChuyenXes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaChuyenXe,MaTuyenXe,TenChuyenXe,NgayGioChay,TaiXe")] ChuyenXe chuyenXe)
        {
            if (ModelState.IsValid)
            {
                chuyenXeService.CapNhatChuyenXe(chuyenXe);

                return RedirectToAction("Index");
            }
            ViewBag.MaTuyenXe = new SelectList(tuyenXeService.XemTuyenXe(), "MaTuyenXe", "TenTuyenXe", chuyenXe.MaTuyenXe);
            return View(chuyenXe);
        }

        // GET: ChuyenXes/Delete/5
        public ActionResult Delete(int id)
        {
            //    if (id == null)
            //    {
            //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //    }
            ChuyenXe chuyenXe = chuyenXeService.LayChuyenXe(id);
            if (chuyenXe == null)
            {
                return HttpNotFound();
            }
            return View(chuyenXe);
        }

        // POST: ChuyenXes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            chuyenXeService.XoaChuyenXe(id);
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
