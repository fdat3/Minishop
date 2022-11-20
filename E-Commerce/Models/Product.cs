using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class Product
    {
        public static List<SanPham>  getProduct()
        {
            //Declare
            List <SanPham> sp = new List<SanPham>();
            //Using Database framework
            QuanLySanPhamEntities db = new QuanLySanPhamEntities();
            //Take value from db
            sp = db.Set<SanPham>().ToList();
            return sp;
        }

        public static List<SanPham> getProductByType(int? MaLoaiSP)
        {
            List<SanPham> sp = new List<SanPham>();
            QuanLySanPhamEntities db = new QuanLySanPhamEntities();
            sp = db.Set<SanPham>().Where(n => n.MaLoaiSP == MaLoaiSP).ToList<SanPham>();
            return sp;
        }

        public static List<LoaiSanPham> getCategories()
        {
            List<LoaiSanPham> lSP = new List<LoaiSanPham>();
            QuanLySanPhamEntities db = new QuanLySanPhamEntities();
            lSP = db.Set<LoaiSanPham>().ToList<LoaiSanPham>();
            return lSP;
        }


        
    }
}