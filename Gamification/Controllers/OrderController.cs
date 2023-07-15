using Gamification.App.Models;
using Gamification.App.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Gamification.App.Models.OrderModels;

namespace Gamification.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize("Bearer")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("not-concluded/{sectorId}")]
        public async Task<IActionResult> GetNotConcludedServices(Guid sectorId)
        {
            return new ResponseHelper().CreateResponse(await _service.GetNotConcludedServices(sectorId));
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
        [Route("update-step")]
        public async Task<IActionResult> UpdateStep(OrderStepUpdate model)
        {

            var userId = User.FindFirst(ClaimTypes.Authentication)!.Value;
            return new ResponseHelper().CreateResponse(await _service.UpdateStep(model, userId));
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
