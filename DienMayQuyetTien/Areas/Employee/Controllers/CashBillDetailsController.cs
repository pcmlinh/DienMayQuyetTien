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
    public class CashBillDetailsController : Controller
    {
        private DmQT07Entities1 db = new DmQT07Entities1();

        // GET: Employee/CashBillDetails
        public PartialViewResult Index()
        {
            if (Session["CashBillDetail"] == null)
                Session["CashBillDetail"] = new List<CashBillDetail>();
            return PartialView(Session["CashBillDetail"]);
        }

        // GET: Employee/CashBillDetails/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CashBillDetail cashBillDetail = db.CashBillDetails.Find(id);
        //    if (cashBillDetail == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(cashBillDetail);
        //}
        public int SalePrice(int ProductID)
        {
            return db.Products.Find(ProductID).SalePrice;
        }
        // GET: Employee/CashBillDetails/Create
        public PartialViewResult Create()
        {
            ViewBag.ProductID = new SelectList(db.Products, "ID", "ProductName");
            var model = new CashBillDetail();
            model.BillID = 0;
            model.Quantity = 1;
            return PartialView(model);
        }

        // POST: Employee/CashBillDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create2(CashBillDetail model)
        {
            if (ModelState.IsValid)
            {
                model.ID = Environment.TickCount;
                model.Product = db.Products.Find(model.ProductID);
                var cashBillDetail = Session["CashBillDetail"] as List<CashBillDetail>;
                if (cashBillDetail == null)
                {
                    cashBillDetail = new List<CashBillDetail>();
                    
                }
                //db.CashBillDetails.Add(cashBillDetail);
                //db.SaveChanges();
                //return RedirectToAction("Index");
                cashBillDetail.Add(model);
                Session["CashBillDetail"] = cashBillDetail;
                return RedirectToAction("Create", "ManageCashBill");
            }

            //ViewBag.BillID = new SelectList(db.CashBills, "ID", "BillCode", cashBillDetail.BillID);
            ViewBag.ProductID = new SelectList(db.Products, "ID", "ProductName", model.ProductID);
            return View("Create",model);
        }

        // GET: Employee/CashBillDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashBillDetail cashBillDetail = db.CashBillDetails.Find(id);
            if (cashBillDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.BillID = new SelectList(db.CashBills, "ID", "BillCode", cashBillDetail.BillID);
            ViewBag.ProductID = new SelectList(db.Products, "ID", "ProductCode", cashBillDetail.ProductID);
            return View(cashBillDetail);
        }

        // POST: Employee/CashBillDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BillID,ProductID,Quantity,SalePrice")] CashBillDetail cashBillDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cashBillDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BillID = new SelectList(db.CashBills, "ID", "BillCode", cashBillDetail.BillID);
            ViewBag.ProductID = new SelectList(db.Products, "ID", "ProductCode", cashBillDetail.ProductID);
            return View(cashBillDetail);
        }

        // GET: Employee/CashBillDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            var CTHoaDon = Session["CashBillDetail"] as List<CashBillDetail>;
            CTHoaDon = CTHoaDon.Except(CTHoaDon.Where(c => c.ID == id)).ToList();
            Session["CashBillDetail"] = CTHoaDon;
            return RedirectToAction("Create", "ManageCashBill");
        }

        // POST: Employee/CashBillDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CashBillDetail cashBillDetail = db.CashBillDetails.Find(id);
            db.CashBillDetails.Remove(cashBillDetail);
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
