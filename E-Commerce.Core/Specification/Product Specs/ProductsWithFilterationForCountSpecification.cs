using E_Commerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Specification.Product_Specs
{
    public class ProductsWithFilterationForCountSpecification:BaseSpecifications<Product>
    {
        public ProductsWithFilterationForCountSpecification(ProductSpecParams _params):base
            (
            P =>
            (string.IsNullOrEmpty(_params.Search) || P.Name.ToLower().Contains(_params.Search)) &&
            (!_params.BrandId.HasValue || P.ProductBrandId == _params.BrandId.Value) &&
            (!_params.TypeId.HasValue || P.ProductTypeId == _params.TypeId.Value))
        {
            
        }

    }
}
