using E_Commerce.Core.Models;

namespace Test_E_CommerceProject.Service.Dtos
{
    public class ProductToReturnDto
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }


        public int ProductBrandId { get; set; }//as foreign Key
        public string ProductBrand { get; set; } //as object

        public int ProductTypeId { get; set; } //as foreign Key
        public string ProductType { get; set; } //as object

    }
}
