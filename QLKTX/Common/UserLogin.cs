using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLKTX.Common
{
    [Serializable]
    public class UserLogin
    {
        public string TenDangNhap { get; set;}
        public string VaiTroID { get; set;}
        public string HoTen { get; set;}
    }
}