using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Models
{
    public class Product:ProductBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }

        //one to many relationship
        public int ProductBrandId { get; set; }//as foreign Key
        public ProductBrand ProductBrand { get; set; } //as object


        //One to Many Relationship
        public int ProductTypeId { get; set; } //as foreign Key
        public ProductType ProductType { get; set; } //as object
    }
}
