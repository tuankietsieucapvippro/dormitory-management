using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLKTX.Common
{
    public class HoaDonViewModel
    {
        public int PhongID { get; set; }
        public string TenPhong { get; set; }
        public string TenToaNha { get; set; }
        public int DienNuocID { get; set; }
        public DateTime NgayLapHD { get; set; }
        public int ChiSoDienCu { get; set; }
        public int ChiSoDienMoi { get; set; }
        public int ChiSoNuocCu { get; set; }
        public int ChiSoNuocMoi { get; set; }
        public int TieuThuDien { get; set; }
        public int TieuThuNuoc { get; set; }
        public float DonGiaDien { get; set; }
        public float DonGiaNuoc { get; set; }
        public float ThanhTienDien { get; set; }
        public float ThanhTienNuoc { get; set; }
        public string TinhTrang { get; set; }
    }
}