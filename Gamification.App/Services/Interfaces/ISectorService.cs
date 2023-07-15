using Gamification.App.Models;

namespace Gamification.App.Services.Interfaces
{
    public interface ISectorService
    {
        Task<ResponseModel> AddAsync(SectorAddModel model);
        Task<ResponseModel> EditAsync(SectorEditModel model);
        Task<ResponseModel> GetOne(Guid id);
        Task<ResponseModel> List();
        Task<ResponseModel> ListPaginate(SectorFilterModel filter);
        Task<ResponseModel> RemoveAsync(Guid id);
    }
}