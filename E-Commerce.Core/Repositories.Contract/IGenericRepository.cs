using E_Commerce.Core.Models;
using E_Commerce.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Repositories.Contract
{
    public interface IGenericRepository<T> where T : ProductBase
    {
        #region Work without specification
        Task<T?> GetAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        #endregion

        #region Work With Specification
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);

        Task<T?> GetWithSpecAsync(ISpecification<T> spec);
        Task<int> GetCount(ISpecification<T> spec);
        #endregion

    }
}
