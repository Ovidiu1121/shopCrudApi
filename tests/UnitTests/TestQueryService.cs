using Moq;
using ShopCrudApi.Shops.Model;
using ShopCrudApi.Shops.Repository.interfaces;
using ShopCrudApi.Shops.Service;
using ShopCrudApi.Shops.Service.interfaces;
using ShopCrudApi.System.Constant;
using ShopCrudApi.System.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tests.Helpers;
using Xunit;

namespace tests.UnitTests
{
    public class TestQueryService
    {

        Mock<IShopRepository> _mock;
        IShopQueryService _service;

        public TestQueryService()
        {
            _mock = new Mock<IShopRepository>();
            _service = new ShopQueryService(_mock.Object);
        }

        [Fact]
        public async Task GetAll_ItemsDoesNotExist()
        {
            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Shop>());

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetAllShops());

            Assert.Equal(Constants.NO_SHOPS_EXIST, exception.Message);

        }

        [Fact]
        public async Task GetAll_ReturnShops()
        {

            var shops = TestShopFactory.CreateShops(5);

            _mock.Setup(repo=>repo.GetAllAsync()).ReturnsAsync(shops);

            var result= await _service.GetAllShops();

            Assert.NotNull(result);
            Assert.Contains(shops[1], result);

        }

        [Fact]
        public async Task GetByName_ItemDoesNotExist()
        {

            _mock.Setup(repo => repo.GetByNameAsync("")).ReturnsAsync((Shop)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetByName(""));

            Assert.Equal(Constants.SHOP_DOES_NOT_EXIST, exception.Message);

        }

        [Fact]
        public async Task GetByName_ReturnShop()
        {

            var shop = TestShopFactory.CreateShop(5);
            shop.Name="test";

            _mock.Setup(repo => repo.GetByNameAsync("test")).ReturnsAsync(shop);

            var result = await _service.GetByName("test");

            Assert.NotNull(result);
            Assert.Equal(shop, result);

        }

        [Fact]
        public async Task GetById_ItemDoesNotExist()
        {

            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Shop)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetById(1));

            Assert.Equal(Constants.SHOP_DOES_NOT_EXIST, exception.Message);

        }

        [Fact]
        public async Task GetById_ReturnShop()
        {

            var shop = TestShopFactory.CreateShop(5);

            _mock.Setup(repo => repo.GetByIdAsync(5)).ReturnsAsync(shop);

            var result = await _service.GetById(5);

            Assert.NotNull(result);
            Assert.Equal(shop, result);

        }


    }
}
