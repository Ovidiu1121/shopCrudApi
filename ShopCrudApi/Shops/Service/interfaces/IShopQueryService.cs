using ShopCrudApi.Dto;
using ShopCrudApi.Shops.Model;

namespace ShopCrudApi.Shops.Service.interfaces
{
    public interface IShopQueryService
    {
        Task<ListShopDto> GetAllShops();
        Task<ShopDto> GetByName(string name);
        Task<ShopDto> GetById(int id);
    }
}
