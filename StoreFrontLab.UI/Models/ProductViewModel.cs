using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoreFrontLab.DATA.EF;

namespace StoreFrontLab.UI.Models
{
    public class ProductViewModel
    {
        public bool IsFeatured { get; set; }
        public Product Product { get; set; }
        public ProductViewModel(Product product)
        {
            Product = product;
            IsFeatured = product.IsFeatured;
        }
    }
}