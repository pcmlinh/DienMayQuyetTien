//------------------------------------------------------------------------------
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
    using System.Web;

    public partial class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Img { get; set; }
        public string Infor { get; set; }
        public Nullable<bool> Check { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }

}
