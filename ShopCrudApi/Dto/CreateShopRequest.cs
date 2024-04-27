namespace ShopCrudApi.Dto
{
    public class CreateShopRequest
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public int Employees { get; set; }
    }
}
