using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using DienMayQuyetTien.Models;

namespace DienMayQuyetTien.Areas.Employee.Controllers
{
    public class ManageInstallmentBillController : Controller
    {
        private DmQT07Entities1 db = new DmQT07Entities1();

        // GET: Employee/ManageInstallmentBill
        public ActionResult Index()
        {
            if (Session["username"] != null && Session["authority"].ToString() == "Nhân viên bán hàng")
            {
                return View(db.InstallmentBills.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
        }

        // GET: Employee/ManageInstallmentBill/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstallmentBill installmentBill = db.InstallmentBills.Find(id);
            if (installmentBill == null)
            {
                return HttpNotFound();
            }
            if (Session["username"] != null && Session["authority"].ToString() == "Nhân viên bán hàng")
            {
                return View(installmentBill);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
        }

        // GET: Employee/ManageInstallmentBill/Create
        public ActionResult Create()
        {
            if (Session["username"] != null && Session["authority"].ToString() == "Nhân viên bán hàng")
            {
                return View(Session["InstallmentBill"]);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
        }

        // POST: Employee/ManageInstallmentBill/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InstallmentBill model)
        {
            if (ModelState.IsValid)
            {
                Session["InstallmentBill"] = model;
            }

            if (Session["username"] != null && Session["authority"].ToString() == "Nhân viên bán hàng")
            {
                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create2()
        {
            using (var scope = new TransactionScope())
                try
                {
                    var installmentBill = Session["InstallmentBill"] as InstallmentBill;
                    var installmentBillDetail = Session["InstallmentBillDetail"] as List<InstallmentBillDetail>;

                    db.InstallmentBills.Add(installmentBill);
                    db.SaveChanges();

                    foreach (var detail in installmentBillDetail)
                    {
                        detail.BillID = installmentBill.ID;
                        detail.Product = null;
                        db.InstallmentBillDetails.Add(detail);
                    }
                    db.SaveChanges();
                    scope.Complete();

                    Session["InstallmentBill"] = null;
                    Session["InstallmentBillDetail"] = null;
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            if (Session["username"] != null && Session["authority"].ToString() == "Nhân viên bán hàng")
            {
                return View("Create");
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }

        }
        // GET: Employee/ManageInstallmentBill/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstallmentBill installmentBill = db.InstallmentBills.Find(id);
            if (installmentBill == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "CustomerCode", installmentBill.CustomerID);
            return View(installmentBill);
        }

        // POST: Employee/ManageInstallmentBill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BillCode,CustomerID,Date,Shipper,Note,Method,Period,GrandTotal,Taken,Remain")] InstallmentBill installmentBill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(installmentBill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "CustomerCode", installmentBill.CustomerID);
            return View(installmentBill);
        }

        // GET: Employee/ManageInstallmentBill/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstallmentBill installmentBill = db.InstallmentBills.Find(id);
            if (installmentBill == null)
            {
                return HttpNotFound();
            }
            return View(installmentBill);
        }

        // POST: Employee/ManageInstallmentBill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InstallmentBill installmentBill = db.InstallmentBills.Find(id);
            db.InstallmentBills.Remove(installmentBill);
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
