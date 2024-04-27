using ShopCrudApi.Shops.Model;
using ShopCrudApi.Shops.Repository.interfaces;
using ShopCrudApi.Shops.Service.interfaces;
using ShopCrudApi.System.Constant;
using ShopCrudApi.System.Exceptions;

namespace ShopCrudApi.Shops.Service
{
    public class ShopQueryService: IShopQueryService
    {
        private IShopRepository _repository;

        public ShopQueryService(IShopRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Shop>> GetAllShops()
        {
            IEnumerable<Shop> shops = await _repository.GetAllAsync();

            if (shops.Count().Equals(0))
            {
                throw new ItemDoesNotExist(Constants.NO_SHOPS_EXIST);
            }

            return shops;
        }

        public async Task<Shop> GetById(int id)
        {
            Shop shop = await _repository.GetByIdAsync(id);

            if (shop == null)
            {
                throw new ItemDoesNotExist(Constants.SHOP_DOES_NOT_EXIST);
            }

            return shop;
        }

        public async Task<Shop> GetByName(string name)
        {
            Shop shop = await _repository.GetByNameAsync(name);

            if (shop == null)
            {
                throw new ItemDoesNotExist(Constants.SHOP_DOES_NOT_EXIST);
            }

            return shop;
        }
    }
}
