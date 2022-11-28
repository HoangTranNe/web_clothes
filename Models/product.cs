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
    using System.Web;

    public partial class product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public product()
        {
            this.constracts = new HashSet<constract>();
            this.customer_order = new HashSet<customer_order>();
            this.details_order = new HashSet<details_order>();
        }
    
        public int id_products { get; set; }
        public Nullable<int> id_warehouse { get; set; }
        public Nullable<int> id_category { get; set; }
        public Nullable<int> id_brand { get; set; }
        public string name { get; set; }
        public Nullable<float> price { get; set; }
        public Nullable<int> discount { get; set; }
        public string descibe { get; set; }
        public string images { get; set; }

        public string images_size { get; set; }
    
        public virtual brand brand { get; set; }
        public virtual category category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<constract> constracts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<customer_order> customer_order { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<details_order> details_order { get; set; }
        public virtual warehouse warehouse { get; set; }        
    }
}
