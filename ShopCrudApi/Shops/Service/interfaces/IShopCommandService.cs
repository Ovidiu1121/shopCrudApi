using ShopCrudApi.Dto;
using ShopCrudApi.Shops.Model;

namespace ShopCrudApi.Shops.Service.interfaces
{
    public interface IShopCommandService
    {
        Task<ShopDto> CreateShop(CreateShopRequest request);
        Task<ShopDto> UpdateShop(int id, UpdateShopRequest request);
        Task<ShopDto> DeleteShop(int id);
    }
}
