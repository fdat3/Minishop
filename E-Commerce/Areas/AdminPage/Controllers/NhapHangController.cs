using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Areas.AdminPage.Controllers
{
    public class NhapHangController : Controller
    {
        // GET: AdminPage/NhapHang
        QuanLySanPhamEntities db = new QuanLySanPhamEntities();
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.MaNCC = db.NhaCungCaps;
            ViewBag.ListSP = db.SanPhams;
            return View("Index");
        }
        [HttpPost]
        public ActionResult NhapHang(IEnumerable<ChiTietPhieuNhap> lstModel, PhieuNhap model)
        {
            ViewBag.MaNCC = db.NhaCungCaps;
            ViewBag.ListSP = db.SanPhams;
            model.DaXoa = false;
            db.PhieuNhaps.Add(model);
            db.SaveChanges();
            //truyen MaPN o Phieu Nhap => CTPN
            foreach(var item in lstModel)
            {
                //them vao soluongtonkho neu da co 
                SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == item.MaSP);
                sp.SoLuongTonKho += item.SoLuongNhap;
                //Gan
                item.MaPN = model.MaPN;
            }
            db.ChiTietPhieuNhaps.AddRange(lstModel);
            db.SaveChanges();
            return View("Index");
        }
        [HttpGet]

        public ActionResult XuLyHangTonKho()
        {
            var lsSP = db.SanPhams.Where(n => n.DaXoa == false && n.SoLuongTonKho < 5);
            return View(lsSP);
        }

        [HttpPost]

        public ActionResult NhapDonHang(PhieuNhap model, ChiTietPhieuNhap ctpn)
        {
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
            model.NgayNhap = DateTime.Now;
            model.DaXoa = false;
            db.PhieuNhaps.Add(model);
            db.SaveChanges();
            ctpn.MaPN = model.MaPN;
            SanPham sp = db.SanPhams.Single(n => n.MaSP == ctpn.MaSP);
            sp.SoLuongTonKho += ctpn.SoLuongNhap;
            db.ChiTietPhieuNhaps.Add(ctpn);
            db.SaveChanges();
            return View(sp);
        }
    }
}