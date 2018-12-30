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
    }
}