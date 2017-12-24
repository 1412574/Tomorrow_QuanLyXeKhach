using DataModel;
using DataService;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyXeKhach.Controllers
{
    public class BaoCaoController : Controller
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        IBaoCaoService<BaoCao> service;
        BaoCao _baoCao;

        public BaoCaoController(IBaoCaoService<BaoCao> service, BaoCao baoCao)
        {
            this.service = service;
            _baoCao = baoCao;

        }
        // GET: BaoCao
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ThemBaoCao(BaoCao baoCao)
        {
            logger.Info("Start controller....");
            int status = service.ThemBaoCao(baoCao);
            if (status == 0)
            {
                logger.Info("Status: Success");
                return RedirectToAction("Menu", "BaoCao");
            }
            else
            {
                logger.Info("Status: Fail");
                return Content("Thêm thất bại");
            }
        }
        //GET:
        public ActionResult Menu(BaoCao baoCao)
        {
            var listBaoCao = service.XemBaoCao(baoCao);
            return View(listBaoCao);
        }
        //POST:
        //GET
        public ActionResult XacNhanXoa(int id)
        {
            BaoCao baoCao = service.GetBaoCao(id);
            if (baoCao == null)
            {
                return HttpNotFound();
            }
 
            return View(baoCao);
        }

        [HttpPost, ActionName("XacNhanXoa")]
        [ValidateAntiForgeryToken]
        public ActionResult XoaBaoCao(int id)
        {

            logger.Info("Start controller....");
            int status = service.XoaBaoCao(id);
            if (status == 0)
            {
                logger.Info("Status: Success");
                return RedirectToAction("Menu", "BaoCao");
            }
            else
            {
                logger.Info("Status: Fail");
                return Content("Xóa thất bại");
            }
        }
        //POST:
        public ActionResult Edit(int id)
        {
            var modelView = service.GetBaoCao(id);
            return View(modelView);
        }
        public ActionResult CapNhatBaoCao(BaoCao baoCao)
        {
            logger.Info("Start controller....");
            int status = service.CapNhatBaoCao(baoCao);
            if (status == 0)
            {
                logger.Info("Status: Success");
                return RedirectToAction("Menu", "BaoCao");
            }
            else
            {
                logger.Info("Status: Fail");
                return Content("Cập nhật thất bại");
            }
        }
        //GET:
        public ActionResult Details(int id)
        {
            var modelView = service.GetBaoCao(id);
            return View(modelView);
        }

    }
}
