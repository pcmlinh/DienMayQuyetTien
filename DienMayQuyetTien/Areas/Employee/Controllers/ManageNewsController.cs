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
    public class ManageNewsController : Controller
    {
        private DmQT07Entities1 db = new DmQT07Entities1();

        // GET: Employee/ManageNews
        public ActionResult Index()
        {
            if (Session["username"] != null && Session["authority"].ToString() == "Nhân viên bán hàng")
            {
                return View(db.News.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }

        }

        // GET: Employee/ManageNews/Details/5
        public ActionResult Details(string imageName)
        {
            var path = Server.MapPath("~/Images/" + imageName);
            return File(path, "images");
        }

        // GET: Employee/ManageNews/Create
        public ActionResult Create()
        {
            if (Session["username"] != null && Session["authority"].ToString() == "Nhân viên bán hàng")
            {
                return View(Session["News"]);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
        }

        // POST: Employee/ManageNews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(News news)
        {
            if (news.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Chưa có hình sản phẩm!");
            }
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {
                    db.News.Add(news);

                    if (news.ImageFile != null)
                    {
                        var imageName = System.IO.Path.GetFileName(news.ImageFile.FileName);
                        var path = Server.MapPath("~/Images");
                        path = System.IO.Path.Combine(path, imageName);
                        news.ImageFile.SaveAs(path);
                        news.Img = imageName;
                    }
                    db.SaveChanges();
                    scope.Complete();
                    return RedirectToAction("Index");

                }
            }

            if (Session["username"] != null && Session["authority"].ToString() == "Nhân viên bán hàng")
            {
                return View(news);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }


        }

        // GET: Employee/ManageNews/Edit/5
        public ActionResult Edit(int? id)
        {

            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            if (Session["username"] != null && Session["authority"].ToString() == "Nhân viên bán hàng")
            {
                return View(news);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }

        }

        // POST: Employee/ManageNews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(News news)
        {
            if (news.ImageFile == null)
            {
                News thisNew = db.News.Where(p => p.Id == news.Id).FirstOrDefault();
                news.Img = thisNew.Img;
            }
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {
                    News thisNew = db.News.Where(p => p.Id == news.Id).FirstOrDefault();



                    if (news.ImageFile != null)
                    {
                        var imageName = System.IO.Path.GetFileName(news.ImageFile.FileName);
                        var path = Server.MapPath("~/Images");
                        path = System.IO.Path.Combine(path, imageName);
                        news.ImageFile.SaveAs(path);
                        news.Img = imageName;
                    }
                    db.Entry(thisNew).CurrentValues.SetValues(news);
                    db.SaveChanges();
                    scope.Complete();
                    return RedirectToAction("Index");

                }
            }
            if (Session["username"] != null && Session["authority"].ToString() == "Nhân viên bán hàng")
            {
                return View(news);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
        }

        // GET: Employee/ManageNews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            if (Session["username"] != null && Session["authority"].ToString() == "Nhân viên bán hàng")
            {
                return View(news);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }

        }

        // POST: Employee/ManageNews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            db.News.Remove(news);
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
