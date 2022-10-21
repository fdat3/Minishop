using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class CartItem
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public decimal DonGia { get; set; }
        public int SoLuong { get; set; }
        public string HinhAnh { get; set; }
        public decimal TongTien { get; set; }   

        public CartItem (int iMaSP)
        {
            using (QuanLySanPhamEntities db = new QuanLySanPhamEntities())
            {
                SanPham sp = db.SanPhams.Single(n => n.MaSP == iMaSP);
                this.MaSP = iMaSP;
                this.TenSP = sp.TenSP;
                this.HinhAnh = sp.HinhAnh;
                this.DonGia = sp.DonGia.Value;
                this.SoLuong = 1;
                this.TongTien = DonGia * SoLuong;
            }
        }

        public CartItem(int iMaSP, int sl)
        {
            using (QuanLySanPhamEntities db = new QuanLySanPhamEntities())
            {
                SanPham sp = db.SanPhams.Single(n => n.MaSP == iMaSP);
                this.MaSP = iMaSP;
                this.TenSP = sp.TenSP;
                this.HinhAnh = sp.HinhAnh;
                this.DonGia = sp.DonGia.Value;
                this.SoLuong = sl;
                this.TongTien = DonGia * SoLuong;
            }
        }

        public CartItem()
        {

        }
    }
}