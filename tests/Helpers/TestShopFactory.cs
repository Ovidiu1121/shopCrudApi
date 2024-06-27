using ShopCrudApi.Shops.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopCrudApi.Dto;

namespace tests.Helpers
{
    public class TestShopFactory
    {

        public static ShopDto CreateShop(int id)
        {
            return new ShopDto
            {
                Id = id,
                Name="Emag"+id,
                Location="Sibiu"+id,
                Employees=24+id
            };
        }

        public static ListShopDto CreateShops(int count)
        {
            ListShopDto shops = new ListShopDto();

            for (int i = 0; i<count; i++)
            {
                shops.shopList.Add(CreateShop(i));
            }
            return shops;
        }

    }
}
