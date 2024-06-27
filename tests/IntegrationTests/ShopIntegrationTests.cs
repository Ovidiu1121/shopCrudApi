using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ShopCrudApi.Dto;
using ShopCrudApi.Shops.Model;
using tests.Infrastructure;
using Xunit;

namespace tests.IntegrationTests;

public class ShopIntegrationTests: IClassFixture<ApiWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ShopIntegrationTests(ApiWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }
    
    [Fact]
    public async Task Post_Create_ValidRequest_ReturnsCreatedStatusCode_ValidProductContentResponse()
    {
        var request = "/api/v1/Shop/create";
        var shop = new CreateShopRequest() { Name = "new name", Location = "new location", Employees = 32 };
        var content = new StringContent(JsonConvert.SerializeObject(shop), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Shop>(responseString);

        Assert.NotNull(result);
        Assert.Equal(shop.Name, result.Name);
        Assert.Equal(shop.Location, result.Location);
        Assert.Equal(shop.Employees, result.Employees);
    }
    
    [Fact]
    public async Task Post_Create_ShopAlreadyExists_ReturnsBadRequestStatusCode()
    {
        var request = "/api/v1/Shop/create";
        var shop = new CreateShopRequest() {  Name = "new name", Location = "new location", Employees = 32 };
        var content = new StringContent(JsonConvert.SerializeObject(shop), Encoding.UTF8, "application/json");

        await _client.PostAsync(request, content);
        var response = await _client.PostAsync(request, content);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    [Fact]
    public async Task Put_Update_ValidRequest_ReturnsAcceptedStatusCode_ValidProductContentResponse()
    {
        var request = "/api/v1/Shop/create";
        var shop = new CreateShopRequest { Name = "new name", Location = "new location", Employees = 32 };
        var content = new StringContent(JsonConvert.SerializeObject(shop), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Shop>(responseString)!;

        request = "/api/v1/Shop/update/"+result.Id;
        var updateShop = new UpdateShopRequest() {Name = "updated name", Location = "updated location", Employees = 88 };
        content = new StringContent(JsonConvert.SerializeObject(updateShop), Encoding.UTF8, "application/json");

        response = await _client.PutAsync(request, content);
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        responseString = await response.Content.ReadAsStringAsync();
        result = JsonConvert.DeserializeObject<Shop>(responseString)!;

        Assert.Equal(updateShop.Name, result.Name);
        Assert.Equal(updateShop.Location, result.Location);
        Assert.Equal(updateShop.Employees, result.Employees);
    }
    
    [Fact]
    public async Task Put_Update_ShopDoesNotExists_ReturnsNotFoundStatusCode()
    {
        
        var request = "/api/v1/Shop/update/1";
        var updateShop = new UpdateShopRequest() { Name = "updated name", Location = "updated location", Employees = 88 };
        var content = new StringContent(JsonConvert.SerializeObject(updateShop), Encoding.UTF8, "application/json");

        var response = await _client.PutAsync(request, content);
        
        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
        
    }
    
    [Fact]
    public async Task Delete_Delete_ShopExists_ReturnsDeletedShop()
    {

        var request = "/api/v1/Shop/create";
        var shop = new CreateShopRequest() {  Name = "new name", Location = "new location", Employees = 32 };
        var content = new StringContent(JsonConvert.SerializeObject(shop), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Shop>(responseString)!;

        request = "/api/v1/Shop/delete/" + result.Id;
        response = await _client.DeleteAsync(request);
        
        Assert.Equal(HttpStatusCode.Accepted,response.StatusCode);
    }
    
    [Fact]
    public async Task Delete_Delete_ShopDoesNotExists_ReturnsNotFoundStatusCode()
    {
        var request = "/api/v1/Shop/delete/2";

        var response = await _client.DeleteAsync(request);

        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
    }
    
    [Fact]
    public async Task Get_GetByName_ValidRequest_ReturnsOKStatusCode()
    {

        var request = "/api/v1/Shop/create";
        var shop = new CreateShopRequest() { Name = "new name", Location = "new location", Employees = 32  };
        var content = new StringContent(JsonConvert.SerializeObject(shop), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Shop>(responseString)!;

        request = "/api/v1/Shop/name/" + result.Name;
        response = await _client.GetAsync(request);

        Assert.Equal(HttpStatusCode.OK,response.StatusCode);
    }
    
    [Fact]
    public async Task Get_GetByName_ShopDoesNotExists_ReturnsNotFoundStatusCode()
    {

        var request = "/api/v1/Shop/name/testt";

        var response = await _client.GetAsync(request);
        
        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);

    }
    
    [Fact]
    public async Task Get_GetById_ValidRequest_ReturnsOKStatusCode()
    {

        var request = "/api/v1/Shop/create";
        var shop = new CreateShopRequest() { Name = "new name", Location = "new location", Employees = 32  };
        var content = new StringContent(JsonConvert.SerializeObject(shop), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Shop>(responseString)!;

        request = "/api/v1/Shop/id/" + result.Id;
        response = await _client.GetAsync(request);

        Assert.Equal(HttpStatusCode.OK,response.StatusCode);
    }
    
    [Fact]
    public async Task Get_GetById_ShopDoesNotExists_ReturnsNotFoundStatusCode()
    {

        var request = "/api/v1/Shop/id/55";

        var response = await _client.GetAsync(request);
        
        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);

    }
    
    
}