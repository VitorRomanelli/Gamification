using Gamification.App.Models;
using Gamification.App.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Gamification.App.Models.ConquestModels;
using static Gamification.App.Models.OrderModels;

namespace Gamification.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize("Bearer")]
    public class ConquestController : ControllerBase
    {
        private readonly IConquestService _service;

        public ConquestController(IConquestService service)
        {
            _service = service;
        }


        [HttpGet]
        [Route("byUser/{userId}")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            return new ResponseHelper().CreateResponse(await _service.GetByUserId(userId));
        }

        [HttpGet]
        [Route("bySector/{sectorId}")]
        public async Task<IActionResult> GetByUserId(Guid sectorId)
        {
            return new ResponseHelper().CreateResponse(await _service.GetBySectorId(sectorId));
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
        [Route("list/user/paginate")]
        public async Task<IActionResult> ListPaginateToUser([FromQuery] ConquestFilterModel filter)
        {
            return new ResponseHelper().CreateResponse(await _service.ListPaginateToUser(filter));
        }

        [HttpGet]
        [Route("list/sector/paginate")]
        public async Task<IActionResult> ListPaginateToSector([FromQuery] ConquestFilterModel filter)
        {
            return new ResponseHelper().CreateResponse(await _service.ListPaginateToSector(filter));
        }

        [HttpPost]
        public async Task<IActionResult> Add(ConquestAddModel model)
        {
            return new ResponseHelper().CreateResponse(await _service.AddAsync(model));
        }

        [HttpPut]
        public async Task<IActionResult> Edit(ConquestEditModel model)
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
