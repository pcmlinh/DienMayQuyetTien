using DienMayQuyetTien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace DienMayQuyetTien.Controllers
{
    public class HomeController : Controller
    {
        DmQT07Entities1 db = new DmQT07Entities1();
        public ActionResult Index()
        {
            var product = db.Products.OrderByDescending(x => x.ID).ToList();
            return View(product);
        }
        public FileResult Details(int id)
        {
            var path = Server.MapPath("~/Images/" + id);
            return File(path, "images");
        }
        public ActionResult Detail(int id)
        {
            var product = db.Products.FirstOrDefault(x => x.ID==id);
            return View(product);
        }
        public ActionResult About()
        {

            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }
        public ActionResult Buy()
        {

            return View();
        }
        public ActionResult News()
        {
            var news= db.News.OrderByDescending(x => x.Id).ToList();
            return View(news);
        }
        public ActionResult NewsDetail()
        {

            return View();
        }

    }
}