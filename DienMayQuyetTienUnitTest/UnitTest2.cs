using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DienMayQuyetTien.Models;
using Moq;
using DienMayQuyetTien.Areas.Employee.Controllers;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web;
using System.Web.Routing;
using System.Linq;

namespace DienMayQuyetTienUnitTest
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestIndexCashBill()
        {
            var controller = new ManageCashBillController();
            var context = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(c => c.Session).Returns(session.Object);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            session.Setup(s => s["username"]).Returns("abc");
            session.Setup(s => s["authority"]).Returns("Nhân viên bán hàng");

            var result = controller.Index() as ViewResult;
            var db = new DmQT07Entities1();


            //Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(List<CashBill>));
            Assert.AreEqual(db.CashBills.Count(), ((List<CashBill>)result.Model).Count);

            session.Setup(s => s["username"]).Returns(null);
            session.Setup(s => s["authority"]).Returns(null);
            var redirect = controller.Index() as RedirectToRouteResult;
            //Assert.AreEqual("Login", redirect.RouteValues["controller"]);
            Assert.AreEqual("Login", redirect.RouteValues["action"]);

        }
    }
}