using Gamification.App.Models;
using static Gamification.App.Models.OrderModels;

namespace Gamification.App.Services.Interfaces
{
    public interface IOrderService
    {
        Task<ResponseModel> GetNotConcludedServices(Guid sectorId);
        Task<ResponseModel> UpdateStep(OrderStepUpdate model, string userId);
        Task<ResponseModel> AddAsync(OrderModels.OrderAddModel model);
        Task<ResponseModel> EditAsync(OrderModels.OrderEditModel model);
        Task<ResponseModel> GetOne(Guid id);
        Task<ResponseModel> List();
        Task<ResponseModel> ListPaginate(OrderModels.OrderFilterModel filter);
        Task<ResponseModel> RemoveAsync(Guid id);
        Task<ResponseModel> CheckConquest(string userId);
    }
}