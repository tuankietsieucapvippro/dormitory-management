using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLKTX.Models;

namespace QLKTX.Areas.Admin.Controllers
{
    public class PhongsController : BaseController
    {
        private QLKTXEntities db = new QLKTXEntities();
        public ActionResult FilterRooms(int? toaNhaId, int? loaiPhongId)
        {
            // Bắt đầu với tất cả các phòng
            var filteredRooms = db.Phongs.AsQueryable();

            // Nếu có tòa nhà được chọn, lọc theo tòa nhà
            if (toaNhaId != null)
            {
                filteredRooms = filteredRooms.Where(r => r.ToaNhaID == toaNhaId);
            }

            // Nếu có loại phòng được chọn, lọc theo loại phòng
            if (loaiPhongId != null)
            {
                filteredRooms = filteredRooms.Where(r => r.LoaiPhongID == loaiPhongId);
            }

            // Trả về danh sách phòng đã lọc dưới dạng PartialView
            return PartialView("_RoomList", filteredRooms.ToList());
        }

        public int SoSinhVienDangO(int phongId)
        {
            // Lấy số lượng sinh viên đang ở trong phòng có ID là phongId
            var soSinhVienDangO = db.DKPhongs.Where(s => s.PhongID == phongId).Count();

            return soSinhVienDangO;
        }

        // GET: Admin/Phongs
        public ActionResult Index()
        {
            ViewBag.LOAIPHONGID = new SelectList(db.LoaiPhongs, "LOAIPHONGID", "TENLOAIPHONG");
            ViewBag.TOANHAID = new SelectList(db.ToaNhas, "TOANHAID", "TENTOANHA");

            var phongs = db.Phongs.Include(p => p.LoaiPhong).Include(p => p.ToaNha);
            return View(phongs.ToList());
        }

        // GET: Admin/Phongs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phong phong = db.Phongs.Find(id);
            if (phong == null)
            {
                return HttpNotFound();
            }
            return View(phong);
        }

        // GET: Admin/Phongs/Create
        public ActionResult Create()
        {
            ViewBag.LoaiPhongID = new SelectList(db.LoaiPhongs, "LoaiPhongID", "TenLoaiPhong");
            ViewBag.ToaNhaID = new SelectList(db.ToaNhas, "ToaNhaID", "TenToaNha");
            return PartialView("_CreateRoom");
        }

        // POST: Admin/Phongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PhongID,ToaNhaID,LoaiPhongID,TenPhong,TinhTrang,SoLuongGiuong")] Phong phong)
        {
            if (ModelState.IsValid)
            {
                db.Phongs.Add(phong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LoaiPhongID = new SelectList(db.LoaiPhongs, "LoaiPhongID", "TenLoaiPhong", phong.LoaiPhongID);
            ViewBag.ToaNhaID = new SelectList(db.ToaNhas, "ToaNhaID", "TenToaNha", phong.ToaNhaID);
            return View(phong);
        }

        // GET: Admin/Phongs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phong phong = db.Phongs.Find(id);
            if (phong == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoaiPhongID = new SelectList(db.LoaiPhongs, "LoaiPhongID", "TenLoaiPhong", phong.LoaiPhongID);
            ViewBag.ToaNhaID = new SelectList(db.ToaNhas, "ToaNhaID", "TenToaNha", phong.ToaNhaID);
            return PartialView("_EditRoom",phong);
        }

        // POST: Admin/Phongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PhongID,ToaNhaID,LoaiPhongID,TenPhong,TinhTrang,SoLuongGiuong")] Phong phong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LoaiPhongID = new SelectList(db.LoaiPhongs, "LoaiPhongID", "TenLoaiPhong", phong.LoaiPhongID);
            ViewBag.ToaNhaID = new SelectList(db.ToaNhas, "ToaNhaID", "TenToaNha", phong.ToaNhaID);
            return View(phong);
        }

        // GET: Admin/Phongs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phong phong = db.Phongs.Find(id);
            if (phong == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DeleteRoom",phong);
        }

        // POST: Admin/Phongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Phong phong = db.Phongs.Find(id);
            db.Phongs.Remove(phong);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
