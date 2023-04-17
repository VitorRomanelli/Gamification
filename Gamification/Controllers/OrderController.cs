using Gamification.App.Models;
using Gamification.App.Services.Interfaces;
using keener.Helpers;
using Microsoft.AspNetCore.Mvc;
using static Gamification.App.Models.OrderModels;

namespace Gamification.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOne(Guid id)
        {
            return new ResponseHelper().CreateResponse(await _service.GetOne(id));
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> List()
        {
            return new ResponseHelper().CreateResponse(await _service.List());
        }

        [HttpGet]
        [Route("list/paginate")]
        public async Task<IActionResult> ListPaginate([FromQuery] OrderFilterModel filter)
        {
            return new ResponseHelper().CreateResponse(await _service.ListPaginate(filter));
        }

        [HttpPost]
        public async Task<IActionResult> Add(OrderAddModel model)
        {
            return new ResponseHelper().CreateResponse(await _service.AddAsync(model));
        }

        [HttpPut]
        public async Task<IActionResult> Edit(OrderEditModel model)
        {
            return new ResponseHelper().CreateResponse(await _service.EditAsync(model));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return new ResponseHelper().CreateResponse(await _service.RemoveAsync(id));
        }
    }
}
