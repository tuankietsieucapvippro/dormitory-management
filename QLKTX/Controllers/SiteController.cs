using QLKTX.Common;
using QLKTX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLKTX.Controllers
{
    public class SiteController : Controller
    {
        private QLKTXEntities db = new QLKTXEntities();
        // GET: Site
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InfoSinhVien()
        {
            var userSession = Session["Username"] as UserLogin;
            if (userSession == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var sinhVien = db.SinhViens.FirstOrDefault(sv => sv.MSSV == userSession.TenDangNhap);
            if (sinhVien == null)
            {
                ViewBag.ErrorMessage = "Tài khoản không có thông tin sinh viên.";
                return View("ErrorView");
            }

            return View(sinhVien);
        }

        public ActionResult XemHoaDon()
        {
            // Kiểm tra xem có sinh viên nào đã đăng nhập không
            var user = Session["Username"] as UserLogin;
            if (user != null)
            {
                // Tìm SinhVienID của sinh viên dựa trên MSSV
                var sinhVien = db.SinhViens.FirstOrDefault(sv => sv.MSSV == user.TenDangNhap);
                if (sinhVien != null)
                {
                    // Lấy danh sách hóa đơn của sinh viên từ cơ sở dữ liệu
                    var hoaDonList = (from hd in db.HoaDons
                                      join dn in db.DienNuocs on hd.DienNuocID equals dn.DienNuocID
                                      join dk in db.DKPhongs on hd.PhongID equals dk.PhongID
                                      where dk.SinhVienID == sinhVien.SinhVienID
                                      select new HoaDonViewModel
                                      {
                                          PhongID = (int)hd.PhongID,
                                          TenPhong = dk.Phong.TenPhong,
                                          TenToaNha = dk.Phong.ToaNha.TenToaNha,
                                          DienNuocID = dn.DienNuocID,
                                          NgayLapHD = hd.NgayLapHD,
                                          ChiSoDienCu = (int)dn.ChiSoDienCu,
                                          ChiSoDienMoi = (int)dn.ChiSoDienMoi,
                                          ChiSoNuocCu = (int)dn.ChiSoNuocCu,
                                          ChiSoNuocMoi = (int)dn.ChiSoNuocMoi,
                                          TieuThuDien = (int)dn.ChiSoDienMoi - (int)dn.ChiSoDienCu,
                                          TieuThuNuoc = (int)dn.ChiSoNuocMoi - (int)dn.ChiSoNuocCu,
                                          DonGiaDien = (float)dn.DonGiaDien,
                                          DonGiaNuoc = (float)dn.DonGiaNuoc,
                                          ThanhTienDien = (float)((dn.ChiSoDienMoi - dn.ChiSoDienCu) * dn.DonGiaDien),
                                          ThanhTienNuoc = (float)((dn.ChiSoNuocMoi - dn.ChiSoNuocCu) * dn.DonGiaNuoc),
                                          TinhTrang = hd.TinhTrang
                                      }).ToList();

                    return View(hoaDonList);
                }
                else
                {
                    ViewBag.ErrorMessage = "Tài khoản không có thông tin sinh viên";
                    return View("ErrorView");
                }
            }

            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult CapNhatHoSo()
        {
            var user = Session["Username"] as UserLogin;
            if (user != null)
            {
                var sinhVien = db.SinhViens.FirstOrDefault(sv => sv.MSSV == user.TenDangNhap);
                if (sinhVien != null)
                {
                    var viewModel = new CapNhatHoSoViewModel
                    {
                        SinhVienID = sinhVien.SinhVienID,
                        HoSV = sinhVien.HoSV,
                        TenSV = sinhVien.TenSV,
                        MSSV = sinhVien.MSSV,
                        Lop = sinhVien.Lop,
                        GioiTinh = sinhVien.GioiTinh,
                        NgaySinh = sinhVien.NgaySinh,
                        NoiSinh = sinhVien.NoiSinh,
                        DiaChi = sinhVien.DiaChi,
                        Email = sinhVien.Email,
                        SoDienThoai = sinhVien.SoDienThoai,
                        SoCCCD = sinhVien.SoCCCD
                    };
                    return View(viewModel);
                }
                else
                {
                    ViewBag.ErrorMessage = "Tài khoản không có thông tin sinh viên";
                    return View("ErrorView");
                }
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CapNhatHoSo(CapNhatHoSoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var sinhVien = db.SinhViens.FirstOrDefault(sv => sv.SinhVienID == model.SinhVienID);
                if (sinhVien != null)
                {
                    sinhVien.HoSV = model.HoSV;
                    sinhVien.TenSV = model.TenSV;
                    sinhVien.MSSV = model.MSSV;
                    sinhVien.Lop = model.Lop;
                    sinhVien.GioiTinh = model.GioiTinh;
                    sinhVien.NgaySinh = model.NgaySinh;
                    sinhVien.NoiSinh = model.NoiSinh;
                    sinhVien.DiaChi = model.DiaChi;
                    sinhVien.Email = model.Email;
                    sinhVien.SoDienThoai = model.SoDienThoai;
                    sinhVien.SoCCCD = model.SoCCCD;

                    db.SaveChanges();

                    TempData["SuccessMessage"] = "Cập nhật hồ sơ thành công.";
                    return RedirectToAction("InfoSinhVien","Site");
                }
            }
            return View(model);
        }
        public ActionResult DoiMatKhau()
        {
            var user = Session["Username"] as UserLogin;
            if (user != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Site/DoiMatKhau
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoiMatKhau(DoiMatKhauViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem có sinh viên nào đã đăng nhập không
                var user = Session["Username"] as UserLogin;
                if (user != null)
                {
                    // Lấy thông tin tài khoản từ cơ sở dữ liệu
                    var taiKhoan = db.TaiKhoans.FirstOrDefault(t => t.TenDangNhap == user.TenDangNhap);
                    if (taiKhoan != null)
                    {
                        // Kiểm tra mật khẩu cũ
                        if (taiKhoan.MatKhau == model.MatKhauCu)
                        {
                            // Kiểm tra mật khẩu mới và xác nhận mật khẩu mới
                            if (model.MatKhauMoi == model.XacNhanMatKhauMoi)
                            {
                                // Cập nhật mật khẩu mới
                                taiKhoan.MatKhau = model.MatKhauMoi;
                                db.SaveChanges();

                                // Sử dụng TempData để lưu thông báo thành công
                                TempData["SuccessMessage"] = "Đổi mật khẩu thành công!";
                                return RedirectToAction("InfoSinhVien","Site");
                            }
                            else
                            {
                                TempData["ErrorMessage"] = "Mật khẩu mới và xác nhận mật khẩu mới không khớp.";
                                return View(model);
                            }
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Mật khẩu cũ không đúng.";
                            return View(model);
                        }
                    }
                }
            }

            return View(model);
        }
    }
}