using Gamification.App.Models;
using Gamification.App.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gamification.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize("Bearer")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("change-status")]
        public async Task<IActionResult> GetUserMetrics(ChangeStatusModel model)
        {
            return new ResponseHelper().CreateResponse(await _service.ChangeUserStatusAsync(model));
        }

        [HttpGet]
        [Route("metrics")]
        public async Task<IActionResult> GetUserMetrics([FromQuery] UserFilterModel filter)
        {
            return new ResponseHelper().CreateResponse(await _service.GetUserMetrics(filter));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOne(string id)
        {
            return new ResponseHelper().CreateResponse(await _service.GetOne(id));
        }

        [HttpGet]
        [Route("list/supervisor")]
        public async Task<IActionResult> ListSupervisor()
        {
            return new ResponseHelper().CreateResponse(await _service.ListSupervisor());
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> List()
        {
            return new ResponseHelper().CreateResponse(await _service.List());
        }

        [HttpGet]
        [Route("list/paginate")]
        public async Task<IActionResult> ListPaginate([FromQuery] UserFilterModel filter)
        {
            return new ResponseHelper().CreateResponse(await _service.ListPaginate(filter));
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserAddModel model)
        {
            return new ResponseHelper().CreateResponse(await _service.AddAsync(model));
        }

        [HttpPut]
        public async Task<IActionResult> Edit(UserEditModel model)
        {
            return new ResponseHelper().CreateResponse(await _service.EditAsync(model));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return new ResponseHelper().CreateResponse(await _service.RemoveAsync(id));
        }
    }
}
