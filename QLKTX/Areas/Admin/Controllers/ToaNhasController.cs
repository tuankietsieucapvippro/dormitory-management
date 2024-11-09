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
    public class ToaNhasController : BaseController
    {
        private QLKTXEntities db = new QLKTXEntities();
        public ActionResult GetPhongByToaNhaId(int id)
        {

            var phongs = db.Phongs.Where(p => p.ToaNhaID == id).ToList();
            return PartialView(phongs);

        }

        // GET: Admin/ToaNhas
        public ActionResult Index()
        {
            return View(db.ToaNhas.ToList());
        }

        // GET: Admin/ToaNhas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToaNha toaNha = db.ToaNhas.Find(id);
            if (toaNha == null)
            {
                return HttpNotFound();
            }
            return View(toaNha);
        }

        // GET: Admin/ToaNhas/Create
        public ActionResult Create()
        {
            return PartialView("_CreateModal");
        }

        // POST: Admin/ToaNhas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ToaNhaID,TenToaNha,MoTa")] ToaNha toaNha)
        {
            if (ModelState.IsValid)
            {
                db.ToaNhas.Add(toaNha);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(toaNha);
        }

        // GET: Admin/ToaNhas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToaNha toaNha = db.ToaNhas.Find(id);
            if (toaNha == null)
            {
                return HttpNotFound();
            }
            return PartialView("_EditModal", toaNha);
        }

        // POST: Admin/ToaNhas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ToaNhaID,TenToaNha,MoTa")] ToaNha toaNha)
        {
            if (ModelState.IsValid)
            {
                db.Entry(toaNha).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(toaNha);
        }

        // GET: Admin/ToaNhas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToaNha toaNha = db.ToaNhas.Find(id);
            if (toaNha == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DeleteModel", toaNha);
        }

        // POST: Admin/ToaNhas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ToaNha toaNha = db.ToaNhas.Find(id);
            db.ToaNhas.Remove(toaNha);
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
