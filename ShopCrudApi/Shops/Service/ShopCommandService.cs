using ShopCrudApi.Dto;
using ShopCrudApi.Shops.Model;
using ShopCrudApi.Shops.Repository.interfaces;
using ShopCrudApi.Shops.Service.interfaces;
using ShopCrudApi.System.Constant;
using ShopCrudApi.System.Exceptions;

namespace ShopCrudApi.Shops.Service
{
    public class ShopCommandService: IShopCommandService
    {
        private IShopRepository _repository;

        public ShopCommandService(IShopRepository repository)
        {
            _repository = repository;
        }

        public async Task<ShopDto> CreateShop(CreateShopRequest request)
        {
            ShopDto shop = await _repository.GetByNameAsync(request.Name);

            if (shop!=null)
            {
                throw new ItemAlreadyExists(Constants.SHOP_ALREADY_EXIST);
            }

            shop=await _repository.CreateShop(request);
            return shop;
        }

        public async Task<ShopDto> DeleteShop(int id)
        {
            ShopDto shop = await _repository.GetByIdAsync(id);

            if (shop==null)
            {
                throw new ItemDoesNotExist(Constants.SHOP_DOES_NOT_EXIST);
            }

            await _repository.DeleteShopById(id);
            return shop;
        }

        public async Task<ShopDto> UpdateShop(int id, UpdateShopRequest request)
        {
            ShopDto shop = await _repository.GetByIdAsync(id);

            if (shop==null)
            {
                throw new ItemDoesNotExist(Constants.SHOP_DOES_NOT_EXIST);
            }

            shop = await _repository.UpdateShop(id, request);
            return shop;
        }
    }
}
