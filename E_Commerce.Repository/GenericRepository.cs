using E_Commerce.Core.Models;
using E_Commerce.Core.Repositories.Contract;
using E_Commerce.Core.Specification;
using E_Commerce.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace E_Commerce.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ProductBase
    {
        private readonly StoreContext dbContext;
        public GenericRepository(StoreContext dbContext)//Ask Clr To Inject Object From StoreContext Implicitly
       {
            this.dbContext = dbContext;
        }
        #region Work Without And Specification 
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Product))
            {
                return (IReadOnlyList<T>)await dbContext.Set<Product>().Include(p => p.ProductBrand).Include(P => P.ProductType).ToListAsync();
            }
            return await dbContext.Set<T>().ToListAsync();//query to database
        }

       

        public async Task<T?> GetAsync(int id)
        {
            if (typeof(T) == typeof(Product))
            {
                return await dbContext.Set<Product>().Where(p => p.Id == id).Include(p => p.ProductBrand).Include(p => p.ProductType).FirstOrDefaultAsync() as T;
            }
            return await dbContext.Set<T>().FindAsync(id);
        }


        #endregion

        #region Work With Specification

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        { 
         return await ApplySpecification(spec).ToListAsync();

        }
        public async Task<T?> GetWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();

        }

        public async Task<int> GetCount(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(dbContext.Set<T>(), spec);
        }

        #endregion

    }
}
