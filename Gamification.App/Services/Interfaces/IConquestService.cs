using Gamification.App.Models;

namespace Gamification.App.Services.Interfaces
{
    public interface IConquestService
    {
        Task<ResponseModel> GetByUserId(string userId);
        Task<ResponseModel> GetBySectorId(Guid sectorId);
        Task<ResponseModel> AddAsync(ConquestModels.ConquestAddModel model);
        Task<ResponseModel> EditAsync(ConquestModels.ConquestEditModel model);
        Task<ResponseModel> GetOne(Guid id);
        Task<ResponseModel> List();
        Task<ResponseModel> ListPaginateToUser(ConquestModels.ConquestFilterModel filter);
        Task<ResponseModel> ListPaginateToSector(ConquestModels.ConquestFilterModel filter);
        Task<ResponseModel> RemoveAsync(Guid id);
    }
}