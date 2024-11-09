using QLKTX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLKTX.Common;

namespace QLKTX.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private QLKTXEntities db = new QLKTXEntities();
        // GET: Admin/Login
        public ActionResult Index()
        {
            if (Request.Cookies["tenDangNhap"] != null && Request.Cookies["matKhau"] != null)
            {
                ViewBag.tenDangNhap = Request.Cookies["tenDangNhap"].Value;
                ViewBag.matKhau = Request.Cookies["matKhau"].Value;
            }
            return View();
        }
        public void GhiNhoTaiKhoan(string tenDangNhap, string matKhau)
        {
            HttpCookie tk = new HttpCookie("tenDangNhap");
            HttpCookie mk = new HttpCookie("matKhau");

            tk.Value = tenDangNhap;
            mk.Value = matKhau;

            tk.Expires = DateTime.Now.AddDays(1);
            mk.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(tk);
            Response.Cookies.Add(mk);
        }
        [HttpPost]
        public ActionResult KiemTraDangNhap(string tenDangNhap, string matKhau, string ghiNho)
        {
            if (Request.Cookies["tenDangNhap"] != null && Request.Cookies["matKhau"] != null)
            {
                tenDangNhap = Request.Cookies["tenDangNhap"].Value;
                matKhau = Request.Cookies["matKhau"].Value;
            }

            if (KiemTraMatKhau(tenDangNhap, matKhau))
            {
                var listVaiTro = GetListVaiTroID(tenDangNhap);

                // Kiểm tra xem người dùng có vai trò Admin không
                if (!listVaiTro.Contains("Admin"))
                {
                    // Trả về thông báo lỗi nếu không có vai trò Admin
                    ModelState.AddModelError("", "Bạn không có quyền truy cập.");
                    return View("Index");
                }

                var tkSession = new UserLogin();
                tkSession.TenDangNhap = tenDangNhap;

                Session.Add("SESSION_GROUP", listVaiTro);
                Session.Add("USER_SESSION", tkSession);

                if (ghiNho == "on")
                    GhiNhoTaiKhoan(tenDangNhap, matKhau);
                return Redirect("~/Admin/ToaNhas");
            }
            return Redirect("~/Admin/Login");
        }

        public List<string> GetListVaiTroID(string tenDangNhap)
        {
            var data = (from a in db.VaiTroes
                        join b in db.TaiKhoans on a.VaiTroID equals b.VaiTroID
                        where b.TenDangNhap == tenDangNhap
                        select new
                        {
                            VaiTroID = b.VaiTroID,
                            TenVaiTro = a.TenVaiTro
                        });
            return data.Select(x => x.TenVaiTro).ToList();
        }

        public bool KiemTraMatKhau(string tenDangNhap, string matKhau)
        {
            if (db.TaiKhoans.Where(x => x.TenDangNhap == tenDangNhap && x.MatKhau == matKhau).Count() == 0)
                return false;
            else return true;
        }
        public ActionResult SignOut()
        {
            Session["USER_SESSION"] = null;
            Session["SESSION_GROUP"] = null;

            if (Request.Cookies["tenDangNhap"] != null && Request.Cookies["matKhau"] != null)
            {
                HttpCookie tk = Request.Cookies["tenDangNhap"];
                HttpCookie mk = Request.Cookies["matKhau"];

                tk.Expires = DateTime.Now.AddDays(-1);
                mk.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(tk);
                Response.Cookies.Add(mk);
            }
            return Redirect("~/Admin/Login");
        }
    }
}