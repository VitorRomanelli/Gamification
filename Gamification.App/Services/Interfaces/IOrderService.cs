using Gamification.App.Models;
using keener.Models;

namespace Gamification.App.Services.Interfaces
{
    public interface IOrderService
    {
        Task<ResponseModel> AddAsync(OrderModels.OrderAddModel model);
        Task<ResponseModel> EditAsync(OrderModels.OrderEditModel model);
        Task<ResponseModel> GetOne(Guid id);
        Task<ResponseModel> List();
        Task<ResponseModel> ListPaginate(OrderModels.OrderFilterModel filter);
        Task<ResponseModel> RemoveAsync(Guid id);
    }
}