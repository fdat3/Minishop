//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace E_Commerce.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BinhLuan
    {
        public int MaBL { get; set; }
        public string NoiDung { get; set; }
        public Nullable<int> MaTV { get; set; }
        public Nullable<int> MaSP { get; set; }
        public Nullable<System.DateTime> ThoiGian { get; set; }
    
        public virtual SanPham SanPham { get; set; }
        public virtual ThanhVien ThanhVien { get; set; }
    }
}
