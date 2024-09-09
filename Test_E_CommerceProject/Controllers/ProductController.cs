using AutoMapper;
using E_Commerce.Core.Models;
using E_Commerce.Core.ProductSpec;
using E_Commerce.Core.Repositories.Contract;
using E_Commerce.Core.Specification;
using E_Commerce.Core.Specification.Product_Specs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test_E_CommerceProject.Service.Dtos;
using Test_E_CommerceProject.Service.Errors;
using Test_E_CommerceProject.Service.Helpers;

namespace Test_E_CommerceProject.Api.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IGenericRepository<Product> productsRepo;
        private readonly IGenericRepository<ProductType> typeRepo;
        private readonly IGenericRepository<ProductBrand> brandRepo;
        private readonly IMapper mapper;

        public ProductController(IGenericRepository<Product> ProductsRepo, IGenericRepository<ProductType> Type, IGenericRepository<ProductBrand> Brand, IMapper  mapper)//i want allow Dependancy injection to inject IGenericRepository<Product> ProductsRepo
        {
            productsRepo = ProductsRepo;
            typeRepo = Type;
            brandRepo = Brand;
            this.mapper = mapper;
        }
        #region Without Specification
        //[HttpGet]
        //public async Task<IEnumerable<Product>> GetProducts()
        //{
        //    var products = await productsRepo.GetAllAsync();
        //    return products;
        //}
        //[HttpGet("{Id}")]
        //public async Task<IActionResult> GetProduct(int Id)
        //{

        //    var product = await productsRepo.GetAsync(Id);

        //    return product != null ? Ok(product) : NotFound(new { messsage = "This Product is Not Found", StatusCode = 404 });
        //}
        #endregion

        #region With Specification
        //[HttpGet]
        //public async Task<IEnumerable<Product>> GetProducts()
        //{
        //    var spec = new ProductWithBrandAndTypeSpecification();
        //    var products = await productsRepo.GetAllWithSpecAsync(spec);
        //    return products;
        //}
        //[HttpGet("{Id}")]
        //public async Task<ActionResult<Product>> GetProduct(int Id)
        //{
        //    var spec = new ProductWithBrandAndTypeSpecification(Id);
        //    var product = await productsRepo.GetWithSpecAsync(spec);

        //    return product != null ? Ok(product) : NotFound(new { messsage = "This Product is Not Found", StatusCode = 404 });
        //}
        #endregion

        #region EndPoint Using Mapper =>View 
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams? _params)
        {
            var spec = new ProductWithBrandAndTypeSpecification(_params);
            var products = await productsRepo.GetAllWithSpecAsync(spec);
            var data = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            var specCount = new ProductsWithFilterationForCountSpecification(_params);
            int Count = await productsRepo.GetCount(specCount);
            return Ok(new Pagination<ProductToReturnDto>(_params.PageIndex, _params.PageSize, Count, data));

        }
        [ProducesResponseType(typeof(ProductToReturnDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        [HttpGet("{Id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int Id)
        {
            var spec = new ProductWithBrandAndTypeSpecification(Id);
            var product = await productsRepo.GetWithSpecAsync(spec);

            return product != null ? Ok(mapper.Map<Product, ProductToReturnDto>(product)) : NotFound(new ApiResponse(404));
        }

        [HttpGet("catagories")]//api/product/catagories
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var types = await typeRepo.GetAllAsync();
            return Ok(types);
        }
        [HttpGet("Brands")] //api/product/Brands
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var brands = await brandRepo.GetAllAsync();
            return Ok(brands);
        }
        #endregion




    }
}
