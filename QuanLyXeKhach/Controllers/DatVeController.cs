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
    public class DatVeController : Controller
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        IDatVeService<DatVe> _service;
        IChuyenXeService<ChuyenXe> _serviceSX;
        IKhachHangService<KhachHang> _serviceKH;
        DatVe _dv;

        public DatVeController(IDatVeService<DatVe> service, IChuyenXeService<ChuyenXe> serviceSX,
        IKhachHangService<KhachHang> serviceKH, DatVe dv)
        {
            this._service = service;
            this._serviceSX = serviceSX;
            this._serviceKH = serviceKH;
            _dv = dv;

        }
        // GET: DatVe
        public ActionResult Index()
        {
            var list = _service.xemDatVe();
            return View(list);
        }

        public ActionResult Add()
        {
            IList<ChuyenXe> listChuyenXe = new List<ChuyenXe>();
            listChuyenXe = _serviceSX.XemChuyenXe();
            List<SelectListItem> listMaChuyenXe = new List<SelectListItem>();
            foreach (var cx in listChuyenXe)
            {
                SelectListItem select = new SelectListItem
                {
                    Value = cx.MaChuyenXe.ToString(),
                    Text = cx.TenChuyenXe.ToString()
                };
                listMaChuyenXe.Add(select);
            }
            int firstChuyenXeId = listChuyenXe.First().MaChuyenXe;
            IList<int> gheTrong = new List<int>();
            gheTrong = _serviceSX.danhSachGheTrong(firstChuyenXeId);

            IList<SelectListItem> gheTrongSelect = this.createOptionsForSelectFromList<int>(gheTrong);
            ViewBag.listMaChuyenXe = listMaChuyenXe;
            ViewBag.gheTrongSelect = gheTrongSelect;
            return View();
        }

        public JsonResult LayGheTrongByChuyenXe(int id)
        {
            ChuyenXe cx = _serviceSX.LayChuyenXe(id);
            IList<int> gheTrong = new List<int>();
            gheTrong = _serviceSX.danhSachGheTrong(cx.MaChuyenXe);

            object[] result = new object[2];
            result[0] = gheTrong;
            result[1] = cx.TuyenXe.GiaVe;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ThemDatVe()
        {
            return RedirectToAction("index", "DatVe");
        }

        public ActionResult Edit(int id)
        {
            DatVe dv = _service.layDatVe(id);
            if (dv == null) return HttpNotFound();

            return View(dv);
        }
        public ActionResult CapNhatDatVe(DatVe dv)
        {
            logger.Info("Start controller....");
            int status = _service.capnhatDatVe(dv);
            if (status == 0)
            {
                logger.Info("Status: Success");
                return RedirectToAction("index", "DatVe");
            }
            else
            {
                logger.Info("Status: Fail");
                return Content("Cập nhật thất bại");
            }
        }

        public ActionResult Delete(int id)
        {
            DatVe dv = _service.layDatVe(id);
            if (dv == null) return HttpNotFound();

            return View(dv);
        }

        public ActionResult XoaDatVe(DatVe dv)
        {
            logger.Info("Start controller....");
            int status = _service.xoaDatVe(dv.maDatVe);
            if (status == 0)
            {
                logger.Info("Status: Success");
                return RedirectToAction("index", "KhachHang");
            }
            else
            {
                logger.Info("Status: Fail");
                return Content("Xóa thất bại");
            }
        }
       
        private IList<SelectListItem> createOptionsForSelectFromList<T>(IList<T> list)
        {
            IList<SelectListItem> listItems = new List<SelectListItem>();
            if(typeof(T) == typeof(int))
            {
                foreach(var item in list)
                {
                    SelectListItem select = new SelectListItem
                    {
                        Value = item.ToString(),
                        Text = item.ToString()
                    };
                    listItems.Add(select);
                }
               
            }
            return listItems;
        }
    }
}