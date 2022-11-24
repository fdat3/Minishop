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
        public DateTime NgayDat { get; set; }
        public DateTime NgayGiao { get; set; }


        /*public SortedList<int, ChiTietDonDatHang> itemSelected { get; set; }
        public bool isEmpty()
        {
            return itemSelected.Count == 0;
        }

        public void addItem(int maSP)
        {
            if (itemSelected.Keys.Contains(maSP))
            {
                ChiTietDonDatHang ctdh = itemSelected.Values[itemSelected.IndexOfKey(maSP)];
                ctdh.SoLuong++;
                itemSelected.Values[itemSelected.IndexOfKey(maSP)] = ctdh;
            }
            else
            {
                ChiTietDonDatHang ctd = new ChiTietDonDatHang();
                ctd.MaSP = maSP;
                ctd.SoLuong = 1;
                SanPham sp = Product.getProductByID(maSP);
                ctd.DonGia = DonGia;

                itemSelected.Add(maSP, ctd);
            }


            public void deletedItem(int maSP)
            {
                if (itemSelected.Keys.Contains(maSP))
                {
                    itemSelected.Remove(maSP);
                }
            }

            public void decrease(int maSP)
            {
                if (itemSelected.Keys.Contains(maSP))
                {
                    ChiTietDonDatHang ctd = itemSelected.Values[itemSelected.IndexOfKey(maSP)];
                    if (ctd.SoLuong > 1)
                    {
                        ctd.SoLuong--;
                        itemSelected.Values[itemSelected.IndexOfKey(maSP)] = ctd;
                    }
                    else
                        itemSelected.Remove(maSP);

                }
            }

            public long onePiece(ChiTietDonDatHang ctd)
            {
                return (long)(ctd.DonGia * ctd.SoLuong);
            }

            public long totalBill()
            {
                long bill = 0;
                foreach (ChiTietDonDatHang ctd in itemSelected.Values)
                    bill += onePiece(ctd);
                return bill;
            }*/

        public CartItem(int iMaSP)
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
            /*this.MaSP = MaSP;
            this.TenSP = TenSP;
            this.DonGia = DonGia;
            this.NgayGiao = DateTime.Now.AddDays(2);
            this.NgayDat = DateTime.Now;
            this.itemSelected = new SortedList<int, ChiTietDonDatHang>();*/
        }
    }
}