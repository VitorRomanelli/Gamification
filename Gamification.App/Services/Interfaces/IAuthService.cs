using Gamification.App.Models;
using Gamification.Core.Entities;
using keener.Models;
using Microsoft.IdentityModel.Tokens;

namespace Gamification.App.Services.Interfaces
{
    public interface IAuthService
    {
        SecurityToken GenerateToken(User user);
        Task<ResponseModel> LoginAsync(LoginModel model);
    }
}