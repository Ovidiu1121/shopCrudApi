using ShopCrudApi.Dto;
using ShopCrudApi.Shops.Model;

namespace ShopCrudApi.Shops.Repository.interfaces
{
    public interface IShopRepository
    {
        Task<ListShopDto> GetAllAsync();
        Task<ShopDto> GetByNameAsync(string name);
        Task<ShopDto> GetByIdAsync(int id);
        Task<ShopDto> CreateShop(CreateShopRequest request);
        Task<ShopDto> UpdateShop(int id, UpdateShopRequest request);
        Task<ShopDto> DeleteShopById(int id);
    }
}
