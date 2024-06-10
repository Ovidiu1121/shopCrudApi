using ShopCrudApi.Shops.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tests.Helpers
{
    public class TestShopFactory
    {

        public static Shop CreateShop(int id)
        {
            return new Shop
            {
                Id = id,
                Name="Emag"+id,
                Location="Sibiu"+id,
                Employees=24+id
            };
        }

        public static List<Shop> CreateShops(int count)
        {
            List<Shop> shops = new List<Shop>();

            for (int i = 0; i<count; i++)
            {
                shops.Add(CreateShop(i));
            }
            return shops;
        }

    }
}
