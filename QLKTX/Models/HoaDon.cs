//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLKTX.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class HoaDon
    {
        public int HoaDonID { get; set; }
        public Nullable<int> PhongID { get; set; }
        public Nullable<int> DienNuocID { get; set; }
        public System.DateTime NgayLapHD { get; set; }
        public string TinhTrang { get; set; }
    
        public virtual DienNuoc DienNuoc { get; set; }
        public virtual Phong Phong { get; set; }
    }
}