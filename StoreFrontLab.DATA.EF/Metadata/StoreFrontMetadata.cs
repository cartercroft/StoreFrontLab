using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StoreFrontLab.DATA.EF
{
    class StoreFrontMetadata
    {
        #region Product Metadata
        public class ProductMetadata
        {
            //public int ProductID { get; set; }
            [Required(ErrorMessage = "Value is a required field.")]
            [StringLength(100, ErrorMessage = "Value must be 100 characters or less.")]
            [UIHint("MultilineText")]
            [Display(Name = "Product Name")]
            public string ProductName { get; set; }

            [Range(0, double.MaxValue, ErrorMessage = "Value must be a valid number, 0 or above.")]
            [DisplayFormat(NullDisplayText = "[-N/A-]")]
            public Nullable<decimal> Price { get; set; }

            [Range(0, int.MaxValue)]
            [Display(Name = "Units Sold")]
            public Nullable<int> UnitsSold { get; set; }

            [Required(ErrorMessage = "Type ID is a required field")]
            public int TypeID { get; set; }

            [Display(Name = "Make ID")]
            [DisplayFormat(NullDisplayText = "[-N/A-]")]
            public Nullable<int> MakeID { get; set; }

            [Required(ErrorMessage = "Value is a required field.")]
            [StringLength(25, ErrorMessage = "Value must be 25 characters or less.")]
            public string Model { get; set; }

            [Required(ErrorMessage = "Value is a required field.")]
            [Display(Name = "Product Status ID")]
            public int ProductStatusID { get; set; }

            [DisplayFormat(NullDisplayText = "[-N/A-]")]
            public string Description { get; set; }

            [Display(Name = "Image")]
            public string ProductImage { get; set; }
        }

        [MetadataType(typeof(ProductMetadata))]
        public partial class Product
        {

        }
        #endregion

        #region ProductMake Metadata
        public class ProductMakeMetadata
        {
            [Required(ErrorMessage = "Value is a required field.")]
            public int MakeID { get; set; }

            [Required(ErrorMessage = "Value is a required field.")]
            public string MakeName { get; set; }
        }

        [MetadataType(typeof(ProductMakeMetadata))]
        public partial class ProductMake
        {

        }
        #endregion

        #region ProductStatus Metadata
        public class ProductStatusMetadata
        {
            [Required(ErrorMessage = "Value is a required field.")]
            [Display(Name = "Product Status ID")]
            public int ProductStatusID { get; set; }

            [Required(ErrorMessage = "Value is a required field.")]
            [Display(Name = "Status Name")]
            public string ProductStatusName { get; set; }
        }

        [MetadataType(typeof(ProductStatusMetadata))]
        public partial class ProductStatus
        {

        }
        #endregion

        #region ProductType Metadata
        public class ProductTypeMetadata
        {
            [Required(ErrorMessage = "Value is a required field.")]
            [Display(Name = "Type ID")]
            public int TypeID { get; set; }

            [Required(ErrorMessage = "Value is a required field.")]
            [Display(Name = "Type")]
            public string TypeName { get; set; }
        }
        #endregion
    }
}
