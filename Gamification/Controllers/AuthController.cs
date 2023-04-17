using Gamification.App.Models;
using Gamification.App.Services.Interfaces;
using keener.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Gamification.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {
            return new ResponseHelper().CreateResponse(await _service.LoginAsync(model));
        }
    }
}
