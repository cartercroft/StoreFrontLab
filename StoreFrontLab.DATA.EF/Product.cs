//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StoreFrontLab.DATA.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> UnitsSold { get; set; }
        public int TypeID { get; set; }
        public Nullable<int> MakeID { get; set; }
        public string Model { get; set; }
        public int ProductStatusID { get; set; }
        public string Description { get; set; }
        public string ProductImage { get; set; }
    
        public virtual ProductMake ProductMake { get; set; }
        public virtual ProductStatus ProductStatus { get; set; }
        public virtual ProductType ProductType { get; set; }
    }
}