using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopCrudApi.Dto;
using ShopCrudApi.Shops.Controller;
using ShopCrudApi.Shops.Controller.interfaces;
using ShopCrudApi.Shops.Model;
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
    public class TestController
    {
        Mock<IShopCommandService> _command;
        Mock<IShopQueryService> _query;
        ShopApiController _controller;

        public TestController()
        {

            _command = new Mock<IShopCommandService>();
            _query = new Mock<IShopQueryService>();
            _controller = new ShopController(_command.Object, _query.Object);
        
        }

        [Fact]
        public async Task GetAll_ItemsDoNotExist()
        {

            _query.Setup(repo => repo.GetAllShops()).ThrowsAsync(new ItemDoesNotExist(Constants.SHOP_DOES_NOT_EXIST));

            var result = await _controller.GetAll();

            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(404, notFound.StatusCode);
            Assert.Equal(Constants.SHOP_DOES_NOT_EXIST, notFound.Value);

        }

        [Fact]
        public async Task GetAll_ValidData()
        {

            var shops = TestShopFactory.CreateShops(5);

            _query.Setup(repo => repo.GetAllShops()).ReturnsAsync(shops);

            var result = await _controller.GetAll();

            var okresult = Assert.IsType<OkObjectResult>(result.Result);

            var shopsAll = Assert.IsType<ListShopDto>(okresult.Value);

            Assert.Equal(5, shopsAll.shopList.Count);
            Assert.Equal(200, okresult.StatusCode);


        }

        [Fact]
        public async Task Create_InvalidData()
        {

            var create = new CreateShopRequest
            {
                Name = "test",
                Location = "test",
                Employees = 0
            };

            _command.Setup(repo => repo.CreateShop(It.IsAny<CreateShopRequest>())).ThrowsAsync(new ItemAlreadyExists(Constants.SHOP_ALREADY_EXIST));

            var result = await _controller.CreateShop(create);

            var bad = Assert.IsType<BadRequestObjectResult>(result.Result);

            Assert.Equal(400, bad.StatusCode);
            Assert.Equal(Constants.SHOP_ALREADY_EXIST, bad.Value);

        }

        [Fact]
        public async Task Create_ValidData()
        {

            var create = new CreateShopRequest
            {
                Name = "test",
                Location = "test",
                Employees = 10
            };

            var shop = TestShopFactory.CreateShop(1);

            shop.Name=create.Name;
            shop.Location=create.Location;
            shop.Employees=create.Employees;

            _command.Setup(repo => repo.CreateShop(create)).ReturnsAsync(shop);

            var result = await _controller.CreateShop(create);

            var okResult = Assert.IsType<CreatedResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 201);
            Assert.Equal(shop, okResult.Value);

        }

        [Fact]
        public async Task Update_InvalidDate()
        {

            var update = new UpdateShopRequest
            {
                Name = "Test",
                Location = "test",
                Employees = 1
            };

            _command.Setup(repo => repo.UpdateShop(1, update)).ThrowsAsync(new ItemDoesNotExist(Constants.SHOP_DOES_NOT_EXIST));

            var result = await _controller.UpdateShop(1, update);

            var bad = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(bad.StatusCode, 404);
            Assert.Equal(bad.Value, Constants.SHOP_DOES_NOT_EXIST);

        }

        [Fact]
        public async Task Update_ValidData()
        {

            var update = new UpdateShopRequest
            {
                Name = "Test",
                Location = "test",
                Employees = 300
            };

            var shop = TestShopFactory.CreateShop(5);

            shop.Name=update.Name;
            shop.Location=update.Location;
            shop.Employees=update.Employees.Value;

            _command.Setup(repo => repo.UpdateShop(5, update)).ReturnsAsync(shop);

            var result = await _controller.UpdateShop(5, update);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(okResult.Value, shop);

        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {

            _command.Setup(repo => repo.DeleteShop(5)).ThrowsAsync(new ItemDoesNotExist(Constants.SHOP_DOES_NOT_EXIST));

            var result = await _controller.DeleteShop(5);

            var notfound = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(notfound.StatusCode, 404);
            Assert.Equal(notfound.Value, Constants.SHOP_DOES_NOT_EXIST);

        }

        [Fact]
        public async Task Delete_ValidData()
        {

            var shop = TestShopFactory.CreateShop(1);

            _command.Setup(repo => repo.DeleteShop(1)).ReturnsAsync(shop);

            var result = await _controller.DeleteShop(1);

            var okResult = Assert.IsType<AcceptedResult>(result.Result);

            Assert.Equal(202, okResult.StatusCode);
            Assert.Equal(shop, okResult.Value);

        }

        [Fact]
        public async Task GetByName_ItemDoesNotExist()
        {
            _query.Setup(repo => repo.GetByName("")).ThrowsAsync(new ItemDoesNotExist(Constants.SHOP_DOES_NOT_EXIST));

            var result = await _controller.GetByNameRoute("");

            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(404, notFound.StatusCode);
            Assert.Equal(Constants.SHOP_DOES_NOT_EXIST, notFound.Value);


        }

        [Fact]
        public async Task GetByName_ReturnShop()
        {

            var shop = TestShopFactory.CreateShop(1);

            shop.Name="test";

            _query.Setup(repo => repo.GetByName("test")).ReturnsAsync(shop);

            var result = await _controller.GetByNameRoute("test");

            var okresult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(200, okresult.StatusCode);


        }



    }
}
