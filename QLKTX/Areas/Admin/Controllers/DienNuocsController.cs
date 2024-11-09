using QLKTX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLKTX.Common;

namespace QLKTX.Areas.Admin.Controllers
{
    public class DienNuocsController : BaseController
    {
        // GET: Admin/DienNuocs
        private QLKTXEntities db = new QLKTXEntities();

        public ActionResult Index()
        {
            var danhSachPhong = db.Phongs.ToList();

            // Tạo danh sách ViewModel để chứa thông tin phòng và lần ghi gần nhất
            var danhSachPhongViewModel = new List<PhongViewModel>();

            // Duyệt qua từng phòng và lấy thông tin lần ghi gần nhất của phòng
            foreach (var phong in danhSachPhong)
            {
                var lanGhiGanNhat = db.DienNuocs
                                        .Where(d => d.PhongID == phong.PhongID)
                                        .OrderByDescending(d => d.DenNgay)
                                        .FirstOrDefault();

                var phongViewModel = new PhongViewModel
                {
                    PhongID = phong.PhongID,
                    TenPhong = phong.TenPhong,
                    TenToaNha = phong.ToaNha.TenToaNha,
                    ChiSoDienCu = lanGhiGanNhat?.ChiSoDienCu,
                    ChiSoDienMoi = lanGhiGanNhat?.ChiSoDienMoi,
                    ChiSoNuocCu = lanGhiGanNhat?.ChiSoNuocCu,
                    ChiSoNuocMoi = lanGhiGanNhat?.ChiSoNuocMoi
                };

                danhSachPhongViewModel.Add(phongViewModel);
            }
            danhSachPhongViewModel = danhSachPhongViewModel.OrderBy(d => d.TenToaNha).ThenBy(d => d.TenPhong).ToList();

            return View(danhSachPhongViewModel);
        }

        public ActionResult GhiChiSo(int phongID)
        {
            ViewBag.PhongID = phongID;
            return View();
        }

        [HttpPost]
        public ActionResult GhiChiSo(int phongID, int chiSoDienMoi, int chiSoNuocMoi)
        {
            var lanGhiTruoc = db.DienNuocs
                         .Where(d => d.PhongID == phongID)
                         .OrderByDescending(d => d.DenNgay)
                         .FirstOrDefault();

            // Tạo bản ghi mới cho lần ghi hiện tại
            DienNuoc dienNuoc = new DienNuoc
            {
                PhongID = phongID,
                TuNgay = lanGhiTruoc != null ? lanGhiTruoc.DenNgay : DateTime.Now.AddDays(-1),
                DenNgay = DateTime.Now,
                ChiSoDienCu = lanGhiTruoc != null ? lanGhiTruoc.ChiSoDienMoi : 0,
                ChiSoNuocCu = lanGhiTruoc != null ? lanGhiTruoc.ChiSoNuocMoi : 0,
                ChiSoDienMoi = chiSoDienMoi,
                ChiSoNuocMoi = chiSoNuocMoi,
                DonGiaDien = 5000,
                DonGiaNuoc = 15000
            };

            // Thêm bản ghi mới vào cơ sở dữ liệu
            db.DienNuocs.Add(dienNuoc);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult TaoHoaDon(int phongID)
        {
            var phong = db.Phongs.FirstOrDefault(p => p.PhongID == phongID);

            if (phong == null)
            {
                return HttpNotFound();
            }

            var lanGhiGanNhat = db.DienNuocs
                         .Where(d => d.PhongID == phongID)
                         .OrderByDescending(d => d.DenNgay)
                         .FirstOrDefault();

            if (lanGhiGanNhat == null)
            {
                return HttpNotFound("No meter readings found for the specified room.");
            }

            var hoaDonViewModel = new HoaDonViewModel
            {
                PhongID = phong.PhongID,
                TenPhong = phong.TenPhong,
                TenToaNha = phong.ToaNha.TenToaNha,
                DienNuocID = lanGhiGanNhat.DienNuocID,
                NgayLapHD = DateTime.Now,
                ChiSoDienCu = lanGhiGanNhat.ChiSoDienCu ?? 0,
                ChiSoDienMoi = lanGhiGanNhat.ChiSoDienMoi ?? 0,
                ChiSoNuocCu = lanGhiGanNhat.ChiSoNuocCu ?? 0,
                ChiSoNuocMoi = lanGhiGanNhat.ChiSoNuocMoi ?? 0, 
                DonGiaDien = (float)(lanGhiGanNhat.DonGiaDien ?? 5000f), 
                DonGiaNuoc = (float)(lanGhiGanNhat.DonGiaNuoc ?? 15000f)
            };

            hoaDonViewModel.TieuThuDien = hoaDonViewModel.ChiSoDienMoi - hoaDonViewModel.ChiSoDienCu;
            hoaDonViewModel.TieuThuNuoc = hoaDonViewModel.ChiSoNuocMoi - hoaDonViewModel.ChiSoNuocCu;
            hoaDonViewModel.ThanhTienDien = hoaDonViewModel.TieuThuDien * hoaDonViewModel.DonGiaDien;
            hoaDonViewModel.ThanhTienNuoc = hoaDonViewModel.TieuThuNuoc * hoaDonViewModel.DonGiaNuoc;

            return View(hoaDonViewModel);
        }

        // POST: QuanLyDienNuoc/TaoHoaDon
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TaoHoaDon(HoaDonViewModel model)
        {
            if (ModelState.IsValid)
            {
                var hoaDon = new HoaDon
                {
                    PhongID = model.PhongID,
                    DienNuocID = model.DienNuocID,
                    NgayLapHD = DateTime.Now,
                    TinhTrang = "Chưa thanh toán"
                };

                db.HoaDons.Add(hoaDon);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}