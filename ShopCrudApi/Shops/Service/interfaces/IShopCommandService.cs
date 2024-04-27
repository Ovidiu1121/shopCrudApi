using ShopCrudApi.Dto;
using ShopCrudApi.Shops.Model;

namespace ShopCrudApi.Shops.Service.interfaces
{
    public interface IShopCommandService
    {
        Task<Shop> CreateShop(CreateShopRequest request);
        Task<Shop> UpdateShop(int id, UpdateShopRequest request);
        Task<Shop> DeleteShop(int id);
    }
}
