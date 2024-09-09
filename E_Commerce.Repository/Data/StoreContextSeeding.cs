using E_Commerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Data
{
    public static class StoreContextSeeding
    {

        public static async Task SeedingAsync(StoreContext dbContext)
        {
            if (!dbContext.ProductBrands.Any())//true if one element inside collection
            {
                var brandsData = File.ReadAllText("../E_Commerce.Repository/Data/DataSeeding/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if (brands?.Count > 0)
                {
                    foreach (var brand in brands)
                        await dbContext.ProductBrands.AddAsync(brand);
                    await dbContext.SaveChangesAsync();
                }
            }

            if (!dbContext.ProductTypes.Any())//true if one element inside collection
            {
                var TypesData = File.ReadAllText("../E_Commerce.Repository/Data/DataSeeding/types.json");
                var tpyes = JsonSerializer.Deserialize<List<ProductType>>(TypesData);
                if (tpyes?.Count > 0)
                {
                    foreach (var type in tpyes)
                        await dbContext.ProductTypes.AddAsync(type);
                    await dbContext.SaveChangesAsync();

                }
            }
            if (!dbContext.Products.Any())//true if one element inside collection
            {
                var Products = File.ReadAllText("../E_Commerce.Repository/Data/DataSeeding/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(Products);
                if (products?.Count > 0)
                {
                    foreach (var product in products)
                        await dbContext.Products.AddAsync(product);
                    await dbContext.SaveChangesAsync();

                }
            }


        }

    }
}
