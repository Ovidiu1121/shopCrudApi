using AutoMapper;
using ShopCrudApi.Dto;
using ShopCrudApi.Shops.Model;

namespace ShopCrudApi.Mappings
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateShopRequest, Shop>();
            CreateMap<UpdateShopRequest, Shop>();
        }
    }
}
