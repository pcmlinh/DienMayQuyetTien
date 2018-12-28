using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DienMayQuyetTien.Models;

namespace DienMayQuyetTien.Areas.Employee.Controllers
{
    public class ManageProductTypeController : Controller
    {
        private DmQT07Entities1 db = new DmQT07Entities1();

        // GET: Admin/ManageProductType
        public ActionResult Index()
        {
            if (Session["username"] != null && Session["authority"].ToString() == "Nhân viên bán hàng")
            {
                return View(db.ProductTypes.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            
        }

        // GET: Admin/ManageProductType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductType productType = db.ProductTypes.Find(id);
            if (productType == null)
            {
                return HttpNotFound();
            }
            if (Session["username"] != null && Session["authority"].ToString() == "Nhân viên bán hàng")
            {
                return View(productType);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            
        }

        // GET: Admin/ManageProductType/Create
        public ActionResult Create()
        {
            if (Session["username"] != null && Session["authority"].ToString() == "Nhân viên bán hàng")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
        }

        // POST: Admin/ManageProductType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProductTypeCode,ProductTypeName")] ProductType productType)
        {
            if (ModelState.IsValid)
            {
                db.ProductTypes.Add(productType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (Session["username"] != null && Session["authority"].ToString() == "Nhân viên bán hàng")
            {
                return View(productType);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
        }

        // GET: Admin/ManageProductType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductType productType = db.ProductTypes.Find(id);
            if (productType == null)
            {
                return HttpNotFound();
            }
            if (Session["username"] != null && Session["authority"].ToString() == "Nhân viên bán hàng")
            {
                return View(productType);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
        }

        // POST: Admin/ManageProductType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProductTypeCode,ProductTypeName")] ProductType productType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (Session["username"] != null && Session["authority"].ToString() == "Nhân viên bán hàng")
            {
                return View(productType);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
        }

        // GET: Admin/ManageProductType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductType productType = db.ProductTypes.Find(id);
            if (productType == null)
            {
                return HttpNotFound();
            }
            if (Session["username"] != null && Session["authority"].ToString() == "Nhân viên bán hàng")
            {
                return View(productType);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
        }

        // POST: Admin/ManageProductType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductType productType = db.ProductTypes.Find(id);
            db.ProductTypes.Remove(productType);
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
