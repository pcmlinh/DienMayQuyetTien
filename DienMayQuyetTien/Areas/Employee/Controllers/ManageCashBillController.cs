using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using DienMayQuyetTien.Areas.Employee.Models;
using DienMayQuyetTien.Models;

namespace DienMayQuyetTien.Areas.Employee.Controllers
{
    public class ManageCashBillController : Controller
    {
        private DmQT07Entities1 db = new DmQT07Entities1();

        // GET: Employee/ManageCashBill
        public ActionResult Index()
        {
            if (Session["username"] != null && Session["authority"].ToString() == "Nhân viên bán hàng")
            {
                return View(db.CashBills.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            
        }

        // GET: Employee/ManageCashBill/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashBill cashBill = db.CashBills.Find(id);
            if (cashBill == null)
            {
                return HttpNotFound();
            }
            if (Session["username"] != null && Session["authority"].ToString() == "Nhân viên bán hàng")
            {
                return View(cashBill);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
        }

        // GET: Employee/ManageCashBill/Create
        public ActionResult Create()
        {
            if (Session["username"] != null && Session["authority"].ToString() == "Nhân viên bán hàng")
            {
                return View(Session["CashBill"]);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
        }

        // POST: Employee/ManageCashBill/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CashBill model)
        {
            if (ModelState.IsValid)
            {
                //db.CashBills.Add(cashBill);
                //db.SaveChanges();
                //return RedirectToAction("Index");
                Session["CashBill"] = model;
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
                    var cashBill = Session["CashBill"] as CashBill;
                    var cashBillDetail = Session["CashBillDetail"] as List<CashBillDetail>;

                    db.CashBills.Add(cashBill);
                    db.SaveChanges();

                    foreach (var detail in cashBillDetail)
                    {
                        detail.BillID = cashBill.ID;
                        detail.Product = null;
                        db.CashBillDetails.Add(detail);
                    }
                    db.SaveChanges();
                    scope.Complete();

                    Session["CashBill"] = null;
                    Session["CashBillDetail"] = null;
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
        // GET: Employee/ManageCashBill/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashBill cashBill = db.CashBills.Find(id);
            if (cashBill == null)
            {
                return HttpNotFound();
            }
            if (Session["username"] != null && Session["authority"].ToString() == "Nhân viên bán hàng")
            {
                return View(cashBill);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
        }

        // POST: Employee/ManageCashBill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BillCode,CustomerName,PhoneNumber,Address,Date,Shipper,Note,GrandTotal")] CashBill cashBill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cashBill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if(Session["username"] != null && Session["authority"].ToString() == "Nhân viên bán hàng")
            {
                return View(cashBill);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
        }
        public ActionResult Print(int id)
        {
            var hd = db.CashBills.FirstOrDefault(o => o.ID == id);
            if (hd != null)
            {
                CashBillModel cb = new CashBillModel();
                cb.BillCode = hd.BillCode;
                cb.CustomerName = hd.CustomerName;
                cb.PhoneNumber = hd.PhoneNumber;
                cb.Address = hd.Address;
                cb.Date = hd.Date;
                cb.Shipper = hd.Shipper;
                cb.Note = hd.Note;
                cb.GrandTotal = hd.GrandTotal;
                cb.CASHBILLDETAIL = hd.CashBillDetails.ToList();
                return View(cb);
            }
            else
            {
                return View();
            }

        }
    }
}
