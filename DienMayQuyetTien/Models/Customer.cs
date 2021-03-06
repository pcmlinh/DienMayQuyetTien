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

    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            this.InstallmentBills = new HashSet<InstallmentBill>();
        }
    
        public int ID { get; set; }
        public string CustomerCode { get; set; }

        [StringLength(100, ErrorMessage = "Tên khách hàng không vượt quá 100 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập Số lượng")]
        public string CustomerName { get; set; }

        [StringLength(11,MinimumLength =10, ErrorMessage = "Số điện thoại 10 hoặc 11 số")]
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string PhoneNumber { get; set; }

        [StringLength(100, ErrorMessage = "Địa chỉ không vượt quá 100 kí tự")]
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ngày sinh")]
        public Nullable<int> YearOfBirth { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstallmentBill> InstallmentBills { get; set; }
    }
}
