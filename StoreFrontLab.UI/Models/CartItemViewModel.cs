using StoreFrontLab.DATA.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace StoreFrontLab.UI.Models
{
    public class CartItemViewModel
    {
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        public Product Product { get; set; }

        public CartItemViewModel(int quantity, Product product)
        {
            Product = product;
            Quantity = quantity;
        }
    }
}