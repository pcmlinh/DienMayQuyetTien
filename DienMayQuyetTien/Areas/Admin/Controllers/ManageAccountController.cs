using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DienMayQuyetTien.Models;

namespace DienMayQuyetTien.Areas.Admin.Controllers
{
    public class ManageAccountController : Controller
    {
        private DmQT07Entities1 db = new DmQT07Entities1();

        // GET: Admin/ManageAccount
        public ActionResult Index()
        {
            var accounts = db.Accounts.Include(a => a.authority);
            if (Session["username"] != null && Session["authority"].ToString() == "Quản lí")
            {
                return View(accounts.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            
        }

        // GET: Admin/ManageAccount/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            if (Session["username"] != null && Session["authority"].ToString() == "Quản lí")
            {
                return View(account);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            
        }

        // GET: Admin/ManageAccount/Create
        public ActionResult Create()
        {
            ViewBag.authorityID = new SelectList(db.authorities, "id", "type");
            if (Session["username"] != null && Session["authority"].ToString() == "Quản lí")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
        }

        // POST: Admin/ManageAccount/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,username,password,fullname,email,authorityID")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.authorityID = new SelectList(db.authorities, "id", "type", account.authorityID);
            if (Session["username"] != null && Session["authority"].ToString() == "Quản lí")
            {
                return View(account);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
        }

        // GET: Admin/ManageAccount/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            ViewBag.authorityID = new SelectList(db.authorities, "id", "type", account.authorityID);
            if (Session["username"] != null && Session["authority"].ToString() == "Quản lí")
            {
                return View(account);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
        }

        // POST: Admin/ManageAccount/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,username,password,fullname,email,authorityID")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.authorityID = new SelectList(db.authorities, "id", "type", account.authorityID);
            if (Session["username"] != null && Session["authority"].ToString() == "Quản lí")
            {
                return View(account);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
        }

        // GET: Admin/ManageAccount/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            if (Session["username"] != null && Session["authority"].ToString() == "Quản lí")
            {
                return View(account);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
        }

        // POST: Admin/ManageAccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
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
