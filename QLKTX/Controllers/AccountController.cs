using QLKTX.Common;
using QLKTX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLKTX.Controllers
{
    public class AccountController : Controller
    {
        private QLKTXEntities db = new QLKTXEntities();
        
        public ActionResult Login()
        {
            return View();
        }

        // Xử lý đăng nhập
        [HttpPost]
        public ActionResult Login(string tenDangNhap, string matKhau)
        {
            // Kiểm tra thông tin đăng nhập
            if (IsValidUser(tenDangNhap, matKhau))
            {
                // Lưu thông tin đăng nhập vào session hoặc cookie và chuyển hướng đến trang chủ
                var userSession = new UserLogin();
                userSession.TenDangNhap = tenDangNhap;
                var sinhVien = db.SinhViens.FirstOrDefault(sv => sv.MSSV == tenDangNhap);
                if (sinhVien != null)
                {
                    userSession.HoTen = sinhVien.HoSV + " " + sinhVien.TenSV;
                }
                Session.Add("Username", userSession);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không đúng.";
                return View();
            }
        }

        public ActionResult DangKy()
        {
            ViewBag.LoaiPhongID = new SelectList(db.LoaiPhongs, "LoaiPhongID", "TenLoaiPhong");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(SinhVienDangKyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var taiKhoan = new TaiKhoan
                {
                    TenDangNhap = model.MSSV,
                    MatKhau = "1",
                    VaiTroID = 1 // Đặt vai trò mặc định cho sinh viên
                };
                db.TaiKhoans.Add(taiKhoan);
                db.SaveChanges();

                var sinhVien = new SinhVien
                {
                    HoSV = model.HoSV,
                    TenSV = model.TenSV,
                    MSSV = model.MSSV,
                    Lop = model.Lop,
                    GioiTinh = model.GioiTinh,
                    NgaySinh = model.NgaySinh,
                    NoiSinh = model.NoiSinh,
                    DiaChi = model.DiaChi,
                    Email = model.Email,
                    SoDienThoai = model.SoDienThoai,
                    SoCCCD = model.SoCCCD,
                    TaiKhoanID = taiKhoan.TaiKhoanID
                };
                db.SinhViens.Add(sinhVien);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            ViewBag.LoaiPhongID = new SelectList(db.LoaiPhongs, "LoaiPhongID", "TenLoaiPhong");
            return View(model);
        }
        // Hành động cho trang đăng xuất
        public ActionResult Logout()
        {
            // Xóa thông tin đăng nhập từ session hoặc cookie và chuyển hướng đến trang đăng nhập
            Session.Clear(); // Hoặc có thể xóa cookie
            return RedirectToAction("Login");
        }
        private bool IsValidUser(string tenDangNhap, string matKhau)
        {
            // Kiểm tra trong cơ sở dữ liệu xem có tài khoản nào khớp với thông tin đăng nhập không
            var user = db.TaiKhoans.FirstOrDefault(x => x.TenDangNhap == tenDangNhap && x.MatKhau == matKhau);

            // Nếu tìm thấy tài khoản, trả về true
            if (user != null)
            {
                return true;
            }
            // Ngược lại, trả về false
            else
            {
                return false;
            }
        }
    }
}