using DienMayQuyetTien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DienMayQuyetTien.Areas.Employee.Models
{
    public class CashBillModel
    {
        public CashBillModel()
        {
            this.CASHBILLDETAIL = new HashSet<CashBillDetail>();
        }
        public string BillCode { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public System.DateTime Date { get; set; }
        public string Shipper { get; set; }
        public string Note { get; set; }
        public int GrandTotal { get; set; }

        public virtual ICollection<CashBillDetail> CASHBILLDETAIL { get; set; }
    }
}