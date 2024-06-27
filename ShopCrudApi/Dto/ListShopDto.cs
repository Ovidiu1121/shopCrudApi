namespace ShopCrudApi.Dto;

public class ListShopDto
{
    public ListShopDto()
    {
        shopList = new List<ShopDto>();
    }
    
    public List<ShopDto> shopList { get; set; }
}