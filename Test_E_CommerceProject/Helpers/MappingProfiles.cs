using AutoMapper;
using E_Commerce.Core.Models;
using Test_E_CommerceProject.Service.Dtos;
using static System.Net.WebRequestMethods;

namespace Test_E_CommerceProject.Service.Helpers
{
    public class MappingProfiles:Profile
    {

        public MappingProfiles()
        {

            CreateMap<Product, ProductToReturnDto>().ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());

        }

  
    }
}
