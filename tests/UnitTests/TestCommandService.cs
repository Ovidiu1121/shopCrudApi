using Moq;
using ShopCrudApi.Dto;
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
    public class TestCommandService
    {

        Mock<IShopRepository> _mock;
        IShopCommandService _service;

        public TestCommandService()
        {
            _mock = new Mock<IShopRepository>();
            _service = new ShopCommandService(_mock.Object);
        }

        [Fact]
        public async Task Create_InvalidData()
        {

            var create = new CreateShopRequest
            {
                Name="Test",
                Location="test",
                Employees=0
            };

            var shop = TestShopFactory.CreateShop(5);

            _mock.Setup(repo => repo.GetByNameAsync("Test")).ReturnsAsync(shop);

            var exception = await Assert.ThrowsAsync<ItemAlreadyExists>(() => _service.CreateShop(create));

            Assert.Equal(Constants.SHOP_ALREADY_EXIST, exception.Message);

        }

        [Fact]
        public async Task Create_ReturnShop()
        {

            var create = new CreateShopRequest
            {
                Name="Test",
                Location="test",
                Employees=110
            };

            var shop = TestShopFactory.CreateShop(5);

            shop.Name=create.Name;
            shop.Location=create.Location;
            shop.Employees=create.Employees;

            _mock.Setup(repo => repo.CreateShop(It.IsAny<CreateShopRequest>())).ReturnsAsync(shop);

            var result = await _service.CreateShop(create);

            Assert.NotNull(result);
            Assert.Equal(result, shop);

        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {

            _mock.Setup(repo => repo.DeleteShopById(It.IsAny<int>())).ReturnsAsync((ShopDto)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.DeleteShop(5));

            Assert.Equal(exception.Message, Constants.SHOP_DOES_NOT_EXIST);

        }

        [Fact]
        public async Task Delete_ValidData()
        {

            var shop = TestShopFactory.CreateShop(1);

            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(shop);

            var result = await _service.DeleteShop(1);

            Assert.NotNull(result);
            Assert.Equal(shop, result);

        }

        [Fact]
        public async Task Update_ItemDoesNotExist()
        {
            var update = new UpdateShopRequest
            {
                Name = "test",
                Location="Test",
                Employees=0
            };

            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((ShopDto)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.UpdateShop(1, update));

            Assert.Equal(Constants.SHOP_DOES_NOT_EXIST, exception.Message);

        }

        [Fact]
        public async Task Update_ValidData()
        {
            var update = new UpdateShopRequest
            {
                Name = "test",
                Location="Test",
                Employees=230
            };

            var shop = TestShopFactory.CreateShop(5);

            shop.Name=update.Name;
            shop.Location=update.Location;
            shop.Employees=update.Employees.Value;

            _mock.Setup(repo => repo.GetByIdAsync(5)).ReturnsAsync(shop);
            _mock.Setup(repoo => repoo.UpdateShop(It.IsAny<int>(), It.IsAny<UpdateShopRequest>())).ReturnsAsync(shop);

            var result = await _service.UpdateShop(5, update);

            Assert.NotNull(result);
            Assert.Equal(shop, result);

        }


    }
}
