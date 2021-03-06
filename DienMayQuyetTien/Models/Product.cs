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
    using System.Web;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.CashBillDetails = new HashSet<CashBillDetail>();
            this.InstallmentBillDetails = new HashSet<InstallmentBillDetail>();
        }

        public int ID { get; set; }
        public string ProductCode { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Tên sản phẩm không được ít hơn 5 ký tự và vượt quá 100 kí tự")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn loại sản phẩm")]
        public int ProductTypeID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá bán")]
        [DataType(DataType.Currency)]
        public int SalePrice { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá gốc")]
        [DataType(DataType.Currency)]
        public int OriginPrice { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá trả góp")]
        [DataType(DataType.Currency)]
        public int InstallmentPrice { get; set; }

        [Range(1, 100, ErrorMessage = "Số lượng không vượt quá 100 món")]
        [Required(ErrorMessage = "Vui lòng nhập số lượng")]
        public int Quantity { get; set; }
        public string Avatar { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public Nullable<bool> Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CashBillDetail> CashBillDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstallmentBillDetail> InstallmentBillDetails { get; set; }
        public virtual ProductType ProductType { get; set; }
    }
}
