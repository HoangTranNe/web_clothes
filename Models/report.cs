//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace do_an_web.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class report
    {
        public int id_reports { get; set; }
        public Nullable<int> id_customer { get; set; }
        public string subject_customer { get; set; }
        public string contents_customer { get; set; }
    
        public virtual customers_register customers_register { get; set; }
        public virtual customers_register customers_register1 { get; set; }
    }
}
