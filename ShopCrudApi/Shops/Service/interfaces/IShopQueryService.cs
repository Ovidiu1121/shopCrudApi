using ShopCrudApi.Shops.Model;

namespace ShopCrudApi.Shops.Service.interfaces
{
    public interface IShopQueryService
    {
        Task<IEnumerable<Shop>> GetAllShops();
        Task<Shop> GetByName(string name);
        Task<Shop> GetById(int id);
    }
}
