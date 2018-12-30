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
    public class InstallmentBillDetailsController : Controller
    {
        private DmQT07Entities1 db = new DmQT07Entities1();

        // GET: Employee/InstallmentBillDetails
        public PartialViewResult Index()
        {
            if (Session["InstallmentBillDetail"] == null)
                Session["InstallmentBillDetail"] = new List<InstallmentBillDetail>();
            return PartialView(Session["InstallmentBillDetail"]);
        }

        // GET: Employee/InstallmentBillDetails/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    InstallmentBillDetail installmentBillDetail = db.InstallmentBillDetails.Find(id);
        //    if (installmentBillDetail == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(installmentBillDetail);
        //}
        public int InstallmentPrice(int ProductID)
        {
            return db.Products.Find(ProductID).InstallmentPrice;
        }

        // GET: Employee/InstallmentBillDetails/Create
        public PartialViewResult Create()
        {
            ViewBag.ProductID = new SelectList(db.Products, "ID", "ProductName");
            var model = new InstallmentBillDetail();
            model.BillID = 0;
            model.Quantity = 1;
            return PartialView(model);
        }

        // POST: Employee/InstallmentBillDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create2(InstallmentBillDetail model)
        {
            if (ModelState.IsValid)
            {
                model.ID = Environment.TickCount;
                model.Product = db.Products.Find(model.ProductID);
                var installmentBillDetail = Session["InstallmentBillDetail"] as List<InstallmentBillDetail>;
                if (installmentBillDetail == null)
                {
                    installmentBillDetail = new List<InstallmentBillDetail>();

                }
                installmentBillDetail.Add(model);
                Session["InstallmentBillDetail"] = installmentBillDetail;
                return RedirectToAction("Create", "ManageInstallmentBill");
            }

            //ViewBag.BillID = new SelectList(db.InstallmentBills, "ID", "BillCode", installmentBillDetail.BillID);
            ViewBag.ProductID = new SelectList(db.Products, "ID", "ProductName", model.ProductID);
            return View("Create", model);
        }

        // GET: Employee/InstallmentBillDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstallmentBillDetail installmentBillDetail = db.InstallmentBillDetails.Find(id);
            if (installmentBillDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.BillID = new SelectList(db.InstallmentBills, "ID", "BillCode", installmentBillDetail.BillID);
            ViewBag.ProductID = new SelectList(db.Products, "ID", "ProductCode", installmentBillDetail.ProductID);
            return View(installmentBillDetail);
        }

        // POST: Employee/InstallmentBillDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BillID,ProductID,Quantity,InstallmentPrice")] InstallmentBillDetail installmentBillDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(installmentBillDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BillID = new SelectList(db.InstallmentBills, "ID", "BillCode", installmentBillDetail.BillID);
            ViewBag.ProductID = new SelectList(db.Products, "ID", "ProductCode", installmentBillDetail.ProductID);
            return View(installmentBillDetail);
        }

        // GET: Employee/InstallmentBillDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstallmentBillDetail installmentBillDetail = db.InstallmentBillDetails.Find(id);
            if (installmentBillDetail == null)
            {
                return HttpNotFound();
            }
            return View(installmentBillDetail);
        }

        // POST: Employee/InstallmentBillDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InstallmentBillDetail installmentBillDetail = db.InstallmentBillDetails.Find(id);
            db.InstallmentBillDetails.Remove(installmentBillDetail);
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
