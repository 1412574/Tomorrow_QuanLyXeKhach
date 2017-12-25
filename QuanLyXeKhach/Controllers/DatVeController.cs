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
        IChiTietDatVeService<ChiTietDatVe> _serviceCTDV;
        DatVe _dv;

        public DatVeController(IDatVeService<DatVe> service, IChuyenXeService<ChuyenXe> serviceSX, IChiTietDatVeService<ChiTietDatVe> serviceCTDV,
        IKhachHangService<KhachHang> serviceKH, DatVe dv)
        {
            this._service = service;
            this._serviceSX = serviceSX;
            this._serviceKH = serviceKH;
            this._serviceCTDV = serviceCTDV;
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

        public ActionResult ThemDatVe(FormCollection forms)
        {
            int maChuyenXe = Int32.Parse(forms["ChuyenXe"]);
            double giaVe = Double.Parse(forms["giave"]);
            string soDienThoai = forms["sodienthoai"];
            string tenLKhachHang = forms["tenKhachHang"];
            string[] soGheTemp = forms["soghes[]"].Split(',');
            IList<int> soGhe = new List<int>();
            for(int i = 0; i < soGheTemp.Length; i++)
            {
                soGhe.Add(Int32.Parse(soGheTemp[i]));
            }

            KhachHang kh = _serviceKH.layKhachHangBySoDienThoai(soDienThoai, tenLKhachHang);
            DatVe dv = new DatVe();
            dv.maChuyenXe = maChuyenXe;
            dv.maKhachHang = kh.maKhachHang;
            dv.trangThai = false;
            dv.ngayDat = DateTime.Now;
            dv.tongTien = soGhe.Count * giaVe;
            _service.themDatVe(dv);

            foreach(int sg in soGhe)
            {
                ChiTietDatVe ctdv = new ChiTietDatVe();
                ctdv.soGhe = sg;
                ctdv.giaTien = giaVe;
                ctdv.maDatVe = dv.maDatVe;
                _serviceCTDV.themChiTietDatVe(ctdv);
            }

            TempData["Message"] = "Thêm đặt vé thành công";
            return RedirectToAction("index", "DatVe");
        }

        public ActionResult Edit(int id)
        {
            DatVe dv = _service.layDatVe(id);
            if (dv == null) return HttpNotFound();

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

            IList<int> gheTrong = new List<int>();
            gheTrong = _serviceSX.danhSachGheTrong(dv.ChuyenXe.MaChuyenXe);

            IList<SelectListItem> gheTrongSelect = this.createOptionsForSelectFromList<int>(gheTrong);

            string selections = "[";
            foreach (var item in dv.ChiTietDatVes)
            {
                selections += item.soGhe.ToString();
                selections += ",";
            }
            selections = selections.TrimEnd(',');
            selections += "]";
            ViewBag.listMaChuyenXe = listMaChuyenXe;
            ViewBag.selected = dv.ChuyenXe.MaChuyenXe;
            ViewBag.selections = selections;
            return View(dv);
        }
        public ActionResult CapNhatDatVe(int id, FormCollection forms)
        {
            int maChuyenXe = Int32.Parse(forms["ChuyenXe"]);
            double giaVe = Double.Parse(forms["giave"]);
            string[] soGheTemp = forms["soghes[]"].Split(',');
            IList<int> soGhe = new List<int>();
            for (int i = 0; i < soGheTemp.Length; i++)
            {
                soGhe.Add(Int32.Parse(soGheTemp[i]));
            }
            DatVe dv = _service.layDatVe(id);
            dv.maChuyenXe = maChuyenXe;
            dv.tongTien = soGhe.Count * giaVe;
            _service.capnhatDatVe(dv);

            foreach(ChiTietDatVe ct in dv.ChiTietDatVes)
            {
                _serviceCTDV.xoaChiTietDatVe(ct.maCTDV);
            }

            foreach (int sg in soGhe)
            {
                ChiTietDatVe ctdv = new ChiTietDatVe();
                ctdv.soGhe = sg;
                ctdv.giaTien = giaVe;
                ctdv.maDatVe = dv.maDatVe;
                _serviceCTDV.themChiTietDatVe(ctdv);
            }
            TempData["Message"] = "Cập nhật đặt vé thành công";
            return RedirectToAction("index", "DatVe");
        }

        public ActionResult Delete(int id)
        {
            logger.Info("Start controller....");
            DatVe dv = _service.layDatVe(id);
            if (dv == null) return HttpNotFound();

            int status = _service.xoaDatVe(dv.maDatVe);
            if (status == 0)
            {
                logger.Info("Status: Success");
                TempData["Message"] = "Xóa đặt vé thành công";
                return RedirectToAction("index", "DatVe");
            }
            else
            {
                logger.Info("Status: Fail");
                return Content("Xóa thất bại");
            }
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

        public ActionResult ThanhToan(int id)
        {
            DatVe dv = _service.layDatVe(id);
            dv.trangThai = true;
            _service.capnhatDatVe(dv);

            TempData["Message"] = "Cập nhật thanh toán thành công";
            return RedirectToAction("index", "DatVe");
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