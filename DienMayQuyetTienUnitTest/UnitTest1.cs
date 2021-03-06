﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DienMayQuyetTien.Models;
using Moq;
using DienMayQuyetTien.Areas.Employee.Controllers;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web;
using System.Web.Routing;
using System.Linq;
using System.Transactions;

namespace DienMayQuyetTienUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestIndexProduct()
        {
            var controller = new ManageProductController();
            var context = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(c => c.Session).Returns(session.Object);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            session.Setup(s => s["username"]).Returns("abc");
            session.Setup(s => s["authority"]).Returns("Nhân viên bán hàng");

            var result = controller.Index() as ViewResult;
            var db = new DmQT07Entities1();


            //Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(List<Product>));
            Assert.AreEqual(db.Products.Count(), ((List<Product>)result.Model).Count);

            session.Setup(s => s["username"]).Returns(null);
            session.Setup(s => s["authority"]).Returns(null);
            var redirect = controller.Index() as RedirectToRouteResult;
            //Assert.AreEqual("Login", redirect.RouteValues["controller"]);
            Assert.AreEqual("Login", redirect.RouteValues["action"]);
        }
        [TestMethod]
        public void TestDetails()
        {
            var controller = new ManageProductController();
            var context = new Mock<HttpContextBase>();
            context.Setup(c => c.Server.MapPath("~/Images/0")).Returns("~/Images/0");
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var result = controller.Details("0") as FilePathResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("images", result.ContentType);
            Assert.AreEqual("~/Images/0", result.FileName);
        }
        [TestMethod]
        public void TestCreate()
        {
            var controller = new ManageProductController();
            var context = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(c => c.Session).Returns(session.Object);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            session.Setup(s => s["username"]).Returns("abc");
            session.Setup(s => s["authority"]).Returns("Nhân viên bán hàng");

            var result = controller.Create() as ViewResult;

            //Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.ViewData["ProductTypeID"], typeof(SelectList));
        }

        //[TestMethod]
        //public void TestDelete()
        //{
        //    var db = new DmQT07Entities1();
        //    var product = new Product
        //    {
        //        ProductName = "ProductName",
        //        ProductTypeID = db.ProductTypes.First().ID,
        //        SalePrice = 123,
        //        OriginPrice = 123,
        //        InstallmentPrice = 123,
        //        Quantity = 123,
        //        Avatar = ""
        //    };

        //    var controller = new ManageProductController();
        //var context = new Mock<HttpContextBase>();
        //var session = new Mock<HttpSessionStateBase>();
        //session.Setup(s => s["username"]).Returns("abc");
        //session.Setup(s => s["authority"]).Returns("Nhân viên bán hàng");
        //context.Setup(c => c.Session).Returns(session.Object);
        //    controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

        //    using (var scope = new TransactionScope())
        //    {
        //        db.Products.Add(product);
        //        db.SaveChanges();
        //        var count = db.Products.Count();
        //        var result2 = controller.DeleteConfirmed(product.ID) as RedirectToRouteResult;
        //        Assert.IsNotNull(result2);
        //        Assert.AreEqual(count - 1, db.Products.Count());
        //   }
        //}

    }
}