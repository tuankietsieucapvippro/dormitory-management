using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLKTX.Common
{
    public class PhongViewModel
    {
        public int PhongID { get; set; }
        public string TenPhong { get; set; }
        public string TenToaNha { get; set; }
        public int? ChiSoDienCu { get; set; }
        public int? ChiSoDienMoi { get; set; }
        public int? ChiSoNuocCu { get; set; }
        public int? ChiSoNuocMoi { get; set; }
    }
}