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
    
    public partial class constract
    {
        public int id_constracts { get; set; }
        public Nullable<int> id_partners { get; set; }
        public Nullable<int> id_products { get; set; }
        public string price_constracts { get; set; }
        public Nullable<int> quantity { get; set; }
    
        public virtual partner partner { get; set; }
        public virtual partner partner1 { get; set; }
        public virtual product product { get; set; }
        public virtual product product1 { get; set; }
    }
}
