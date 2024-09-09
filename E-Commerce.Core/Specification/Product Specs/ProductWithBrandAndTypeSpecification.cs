using E_Commerce.Core.Models;
using E_Commerce.Core.Specification;
using E_Commerce.Core.Specification.Product_Specs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.ProductSpec
{
    public class ProductWithBrandAndTypeSpecification:BaseSpecifications<Product>
    {
        public ProductWithBrandAndTypeSpecification(ProductSpecParams _params):
        #region Filteration
                    base(P =>
            (string.IsNullOrEmpty(_params.Search)||P.Name.ToLower().Contains(_params.Search))&&
            (!_params.BrandId.HasValue || P.ProductBrandId == _params.BrandId.Value) &&
            (!_params.TypeId.HasValue || P.ProductTypeId == _params.TypeId.Value)) 
        #endregion
        {
            #region Inner Join
            AddIncludes();
            #endregion
            #region Sorting [Ascending Or Descending using [Price and Name]]
            if (!string.IsNullOrEmpty(_params.Sort))
            {
                switch (_params.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(x => x.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(x => x.Price);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }
            }
            else
                AddOrderBy(p => p.Name);
            #endregion

            #region Apply Pagination
            ApplyPagination((_params.PageIndex - 1) * _params.PageSize, _params.PageSize);
            #endregion

        }

      

        public ProductWithBrandAndTypeSpecification(int id) : base(x => x.Id == id)
        {
            AddIncludes();
        }
        private void AddIncludes()
        {
            Includes.Add(x => x.ProductBrand);
            Includes.Add(x => x.ProductType);
        }
    }
}
