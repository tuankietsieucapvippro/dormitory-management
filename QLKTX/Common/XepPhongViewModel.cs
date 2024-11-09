using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLKTX.Common
{
    public class XepPhongViewModel
    {
        public int SinhVienID { get; set; }
        public int? SelectedToaNhaID { get; set; }
        public int? SelectedPhongID { get; set; }
        public List<SelectListItem> ToaNhas { get; set; }
        public List<SelectListItem> Phongs { get; set; }
        public string HoTen { get; set; }
        public string MSSV { get; set; }

        public XepPhongViewModel()
        {
            ToaNhas = new List<SelectListItem>();
            Phongs = new List<SelectListItem>();
        }
    }
}