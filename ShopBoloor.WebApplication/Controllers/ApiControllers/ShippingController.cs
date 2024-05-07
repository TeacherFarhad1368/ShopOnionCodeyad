using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostModule.Application.Contract.PostCalculate;
using PostModule.Application.Contract.StateQuery;

namespace ShopBoloor.WebApplication.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingController : ControllerBase
    {
        private readonly IPostCalculateApplication _calculate;
        private readonly IStateQuery _state;
        public ShippingController(IPostCalculateApplication calculateApplication, IStateQuery state)
        {
            _calculate = calculateApplication;
            _state = state;
        }
        [HttpGet]
        public async Task<List<StateQueryModel>> Get()
        {
            return await _state.GetStatesWithCity();
        }
        [HttpPost]
        public async Task<IActionResult> Post(PostPriceRequestApiModel command)
        {
            var model = await _calculate.CalculatePostForApi(command);
            return Ok(model);
        }
    }
}
