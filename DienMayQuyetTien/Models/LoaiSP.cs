﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DienMayQuyetTien.Models
{
    public class LoaiSP
    {
        public LoaiSP()
        {
            this.PRODUCT = new HashSet<Product>();
        }
        public string ProductTypeCode { get; set; }
        public string ProductTypeName { get; set; }
        public virtual ICollection<Product> PRODUCT { get; set; }
    }

}