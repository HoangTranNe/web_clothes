﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class webClothesEntities : DbContext
    {
        public webClothesEntities()
            : base("name=webClothesEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<brand> brands { get; set; }
        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<constract> constracts { get; set; }
        public virtual DbSet<customer_order> customer_order { get; set; }
        public virtual DbSet<customer> customers { get; set; }
        public virtual DbSet<details_order> details_order { get; set; }
        public virtual DbSet<partner> partners { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<report> reports { get; set; }
        public virtual DbSet<warehouse> warehouses { get; set; }
    }
}
