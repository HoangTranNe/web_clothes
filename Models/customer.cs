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
    
    public partial class customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public customer()
        {
            this.customer_order = new HashSet<customer_order>();
            this.reports = new HashSet<report>();
        }
    
        public int id_customer { get; set; }
        public string name_customer { get; set; }
        public Nullable<int> phone_customer { get; set; }
        public string email_customer { get; set; }
        public string password_customer { get; set; }
        public string gender_customer { get; set; }
        public Nullable<int> age_customer { get; set; }
        public string address_customer { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<customer_order> customer_order { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<report> reports { get; set; }
    }
}
