using Gamification.App.Models;
using Gamification.App.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gamification.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize("Bearer")]
    public class SectorController : ControllerBase
    {
        private readonly ISectorService _service;

        public SectorController(ISectorService service)
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
        public async Task<IActionResult> ListPaginate([FromQuery] SectorFilterModel filter)
        {
            return new ResponseHelper().CreateResponse(await _service.ListPaginate(filter));
        }

        [HttpPost]
        public async Task<IActionResult> Add(SectorAddModel model)
        {
            return new ResponseHelper().CreateResponse(await _service.AddAsync(model));
        }

        [HttpPut]
        public async Task<IActionResult> Edit(SectorEditModel model)
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
