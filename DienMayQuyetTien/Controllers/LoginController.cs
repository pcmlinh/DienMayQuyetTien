﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DienMayQuyetTien.Models;

namespace DienMayQuyetTien.Controllers
{
    public class LoginController : Controller
    {
        private DmQT07Entities1 db = new DmQT07Entities1();

        // GET: Login
        public ActionResult Login()
        {            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Account objUser)
        {
            using (DmQT07Entities1 db = new DmQT07Entities1())
            {
                var obj = db.Accounts.Where(a => a.username.Equals(objUser.username) && a.password.Equals(objUser.password)).FirstOrDefault();
                if (obj != null)
                {
                    Session["authority"] = obj.authority.type.ToString();
                    Session["username"] = obj.fullname.ToString();
                    if (Session["authority"].ToString() == "Kỹ thuật viên")
                    {
                        return RedirectToAction("Index", "ManageAccount", new { Area = "Admin" });
                    }
                    if (Session["authority"].ToString() == "Nhân viên bán hàng")
                    {
                        return RedirectToAction("Index", "ManageProduct", new { Area = "Employee" });
                    }
                        
                }
            }
            return View(objUser);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Login");
        }
    }
}
