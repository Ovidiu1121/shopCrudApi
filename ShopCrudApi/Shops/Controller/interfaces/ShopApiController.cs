using Microsoft.AspNetCore.Mvc;
using ShopCrudApi.Dto;
using ShopCrudApi.Shops.Model;

namespace ShopCrudApi.Shops.Controller.interfaces
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class ShopApiController: ControllerBase
    {

        [HttpGet("all")]
        [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<Shop>))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<IEnumerable<Shop>>> GetAll();

        [HttpPost("create")]
        [ProducesResponseType(statusCode: 201, type: typeof(Shop))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<Shop>> CreateShop([FromBody] CreateShopRequest request);

        [HttpPut("update/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Shop))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<Shop>> UpdateShop([FromRoute] int id, [FromBody] UpdateShopRequest request);

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Shop))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<Shop>> DeleteShop([FromRoute] int id);

        [HttpGet("{name}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Shop))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<Shop>> GetByNameRoute([FromRoute] string name);

    }
}
