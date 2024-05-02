using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostModule.Application.Contract.PostCalculate;

namespace ShopBoloor.WebApplication.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingController : ControllerBase
    {
        private readonly IPostCalculateApplication _calculate;
        public ShippingController(IPostCalculateApplication calculateApplication)
        {
            _calculate = calculateApplication;
        }
        [HttpPost]
        public async Task<IActionResult> Get(PostPriceRequestModel command)
        {
            var model = await _calculate.CalculatePost(command);
            return Ok(model);
        }
    }
}
