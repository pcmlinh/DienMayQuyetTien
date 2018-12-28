using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DienMayQuyetTien.Models;
using System.Transactions;
using System.Data.Entity.Validation;

namespace DienMayQuyetTien.Areas.Employee.Controllers
{
    public class ManageProductController : Controller
    {
        private DmQT07Entities1 db = new DmQT07Entities1();

        // GET: Admin/ManageProduct
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.ProductType);
            if (Session["username"]!= null && Session["authority"].ToString()=="Nhân viên bán hàng")
            {
                return View(products.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
           
        }

        // GET: Admin/ManageProduct/Details/5
        public FileResult Details(string imageName)
        {
            var path = Server.MapPath("~/Images/" + imageName);
            return File(path, "images");
        }

        // GET: Admin/ManageProduct/Create
        public ActionResult Create()
        {
            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "ID", "ProductTypeName");
            if (Session["username"] != null && Session["authority"].ToString() == "Nhân viên bán hàng")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            
        }

        // POST: Admin/ManageProduct/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(Product model)
        {
            if (model.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Chưa có hình sản phẩm!");
            }
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {
                    db.Products.Add(model);

                    if (model.ImageFile != null)
                    {
                        var imageName = System.IO.Path.GetFileName(model.ImageFile.FileName);
                        var path = Server.MapPath("~/Images");
                        path = System.IO.Path.Combine(path, imageName);
                        model.ImageFile.SaveAs(path);
                        model.Avatar = imageName;
                    }
                    db.SaveChanges();
                    scope.Complete();
                    return RedirectToAction("Index");

                }
            }

            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "ID", "ProductTypeName", model.ProductTypeID);
            if (Session["username"] != null && Session["authority"].ToString() == "Nhân viên bán hàng")
            {
                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
        }

        // GET: Admin/ManageProduct/Edit/5
        public ActionResult Edit(int? id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "ID", "ProductTypeName", product.ProductTypeID);
            if (Session["username"] != null && Session["authority"].ToString() == "Nhân viên bán hàng")
            {
                return View(product);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
        }

        // POST: Admin/ManageProduct/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product model)
        {
            if (model.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Chưa có hình sản phẩm!");
            }
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {
                    db.Products.Add(model);

                    if (model.ImageFile != null)
                    {
                        var imageName = System.IO.Path.GetFileName(model.ImageFile.FileName);
                        var path = Server.MapPath("~/Images");
                        path = System.IO.Path.Combine(path, imageName);
                        model.ImageFile.SaveAs(path);
                        model.Avatar = imageName;
                    }
                    db.SaveChanges();
                    scope.Complete();
                    return RedirectToAction("Index");

                }
            }
            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "ID", "ProductTypeCode", model.ProductTypeID);
            if (Session["username"] != null && Session["authority"].ToString() == "Nhân viên bán hàng")
            {
                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
        }

        // GET: Admin/ManageProduct/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            if (Session["username"] != null && Session["authority"].ToString() == "Nhân viên bán hàng")
            {
                return View(product);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
        }

        // POST: Admin/ManageProduct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
