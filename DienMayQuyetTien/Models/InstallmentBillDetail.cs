﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DienMayQuyetTien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class InstallmentBillDetail
    {
        public int ID { get; set; }
        public int BillID { get; set; }
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Số lượng")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Số lượng")]
        public int InstallmentPrice { get; set; }
    
        public virtual InstallmentBill InstallmentBill { get; set; }
        public virtual Product Product { get; set; }
    }
}
