﻿using Microsoft.AspNetCore.Mvc;
using ShopCrudApi.Dto;
using ShopCrudApi.Shops.Controller.interfaces;
using ShopCrudApi.Shops.Model;
using ShopCrudApi.Shops.Service.interfaces;
using ShopCrudApi.System.Exceptions;

namespace ShopCrudApi.Shops.Controller
{
    public class ShopController:ShopApiController
    {
        private IShopCommandService _shopCommandService;
        private IShopQueryService _shopQueryService;

        public ShopController(IShopCommandService shopCommandService, IShopQueryService shopQueryService)
        {
            _shopCommandService = shopCommandService;
            _shopQueryService = shopQueryService;
        }

        public override async Task<ActionResult<ShopDto>> CreateShop([FromBody] CreateShopRequest request)
        {
            try
            {
                var shops = await _shopCommandService.CreateShop(request);

                return Created("Shop-ul a fost creat",shops);
            }
            catch (ItemAlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public override async Task<ActionResult<ShopDto>> DeleteShop([FromRoute] int id)
        {
            try
            {
                var shops = await _shopCommandService.DeleteShop(id);

                return Accepted("", shops);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<ListShopDto>> GetAll()
        {
            try
            {
                var shops = await _shopQueryService.GetAllShops();
                return Ok(shops);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<ShopDto>> GetByNameRoute([FromRoute] string name)
        {

            try
            {
                var shops = await _shopQueryService.GetByName(name);
                return Ok(shops);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<ShopDto>> GetByIdRoute(int id)
        {
            try
            {
                var shop = await _shopQueryService.GetById(id);
                return Ok(shop);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<ShopDto>> UpdateShop([FromRoute] int id, [FromBody] UpdateShopRequest request)
        {
            try
            {
                var shops = await _shopCommandService.UpdateShop(id, request);

                return Ok(shops);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
