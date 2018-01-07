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
    public class TramXeController : Controller
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        ITramXeService<TramXe> tramXeService;

        public TramXeController(ITramXeService<TramXe> tramXeService)
        {
            this.tramXeService = tramXeService;
        }

        // GET: TramXes
        public ActionResult Index()
        {
            var tramXes = tramXeService.XemTramXe();
            return View(tramXes);
        }

        // GET: TramXes/Details/5
        public ActionResult Details(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            TramXe tramXe = tramXeService.LayTramXe(id);
            if (tramXe == null)
            {
                return HttpNotFound();
            }
            return View(tramXe);
        }

        // GET: TramXes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TramXes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTramXe,TenTramXe,DiaChi")] TramXe tramXe)
        {
            logger.Info("Start controller....");
            int status = tramXeService.ThemTramXe(tramXe);
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

        // GET: TramXes/Edit/5
        public ActionResult Edit(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            TramXe tramXe = tramXeService.LayTramXe(id);
            if (tramXe == null)
            {
                return HttpNotFound();
            }
            return View(tramXe);
        }

        // POST: TramXes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTramXe,TenTramXe,DiaChi")] TramXe tramXe)
        {
            if (ModelState.IsValid)
            {
                tramXeService.CapNhatTramXe(tramXe);

                return RedirectToAction("Index");
            }
            return View(tramXe);
        }

        // GET: TramXes/Delete/5
        public ActionResult Delete(int id)
        {
            //    if (id == null)
            //    {
            //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //    }
            TramXe tramXe = tramXeService.LayTramXe(id);
            if (tramXe == null)
            {
                return HttpNotFound();
            }
            return View(tramXe);
        }

        // POST: TramXes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tramXeService.XoaTramXe(id);
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
