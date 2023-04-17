using Gamification.App.Models;
using keener.Models;

namespace Gamification.App.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResponseModel> ChangeUserStatusAsync(ChangeStatusModel model);
        Task<ResponseModel> AddAsync(UserAddModel user);
        Task<ResponseModel> RemoveAsync(string id);
        Task<ResponseModel> EditAsync(UserEditModel user);
        Task<ResponseModel> GetOne(string id);
        Task<ResponseModel> List();
        Task<ResponseModel> ListSupervisor();
        Task<ResponseModel> GetUserMetrics(UserFilterModel filter);
        Task<ResponseModel> ListPaginate(UserFilterModel filter);
    }
}