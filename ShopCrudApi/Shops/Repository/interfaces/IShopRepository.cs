using ShopCrudApi.Dto;
using ShopCrudApi.Shops.Model;

namespace ShopCrudApi.Shops.Repository.interfaces
{
    public interface IShopRepository
    {
        Task<IEnumerable<Shop>> GetAllAsync();
        Task<Shop> GetByNameAsync(string name);
        Task<Shop> GetByIdAsync(int id);
        Task<Shop> CreateShop(CreateShopRequest request);
        Task<Shop> UpdateShop(int id, UpdateShopRequest request);
        Task<Shop> DeleteShopById(int id);
    }
}
