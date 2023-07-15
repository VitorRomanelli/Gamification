using AspNetCore.IQueryable.Extensions.Filter;
using Gamification.App.Extensions;
using Gamification.App.Models;
using Gamification.App.Services.Interfaces;
using Gamification.Application.Extensions;
using Gamification.Core.Entities;
using Gamification.Infra.Context;
using Microsoft.EntityFrameworkCore;
using static Gamification.App.Models.ConquestModels;

namespace Gamification.App.Services
{
    public class ConquestService : IConquestService
    {
        private AppDbContext _context;

        public ConquestService(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<ResponseModel> GetByUserId(string userId)
        {
            var conquests = await _context.Conquests.Where(x => x.Users.Select(x => x.UserId).Contains(userId)).AsNoTracking().Include(x => x.Users).MapToDTO().ToListAsync();
            return ResponseModel.BuildOkResponse(conquests);
        }

        public async Task<ResponseModel> GetBySectorId(Guid sectorId)
        {
            var conquests = await _context.Conquests.Where(x => x.Sectors.Select(x => x.SectorId).Contains(sectorId)).AsNoTracking().Include(x => x.Sectors).MapToDTO().ToListAsync();
            return ResponseModel.BuildOkResponse(conquests);
        }

        public async Task<ResponseModel> GetOne(Guid id)
        {
            var sector = await _context.Conquests.Where(x => x.Id == id).AsNoTracking().MapToDTO().FirstOrDefaultAsync();
            return ResponseModel.BuildOkResponse(sector);
        }

        public async Task<ResponseModel> List()
        {
            var orders = await _context.Conquests.MapToDTO().ToListAsync();
            return ResponseModel.BuildOkResponse(orders);
        }

        public async Task<ResponseModel> ListPaginateToUser(ConquestFilterModel filter)
        {
            return ResponseModel.BuildOkResponse(await _context.Conquests.PersonalFilterToUser(filter).AsNoTracking().MapToDTO().ReturnPaginated(filter.Page));
        }

        public async Task<ResponseModel> ListPaginateToSector(ConquestFilterModel filter)
        {
            return ResponseModel.BuildOkResponse(await _context.Conquests.PersonalFilterToSector(filter).AsNoTracking().MapToDTO().ReturnPaginated(filter.Page));
        }

        public async Task<ResponseModel> AddAsync(ConquestAddModel model)
        {
            var find = await _context.Conquests.FirstOrDefaultAsync(x => x.Title == model.Title);

            if (find != null)
            {
                return ResponseModel.BuildConflictResponse("Já existe uma conquista com esse título!");
            }

            await _context.AddAsync(new Conquest
            {
                Title = model.Title,
                Description = model.Description,
                EndDate = model.EndDate,
                Points = model.Points,
                ServicesConcludedCount = model.ServicesConcludedCount,
                To = model.To,
                Type = model.Type,
            });
            await _context.SaveChangesAsync();

            return ResponseModel.BuildOkResponse("Conquista cadastrada com sucesso!");
        }

        public async Task<ResponseModel> EditAsync(ConquestEditModel model)
        {
            var find = await _context.Conquests.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (find == null)
            {
                return ResponseModel.BuildConflictResponse("Conquista não encontrada no banco de dados!");
            }

            find.Title = model.Title;
            find.Description = model.Description;
            find.EndDate = model.EndDate;
            find.Points = model.Points;
            find.ServicesConcludedCount = model.ServicesConcludedCount;
            find.To = model.To;
            find.Type = model.Type;

            _context.Update(find);
            await _context.SaveChangesAsync();

            return ResponseModel.BuildOkResponse("Conquista atualizada com sucesso!");
        }

        public async Task<ResponseModel> RemoveAsync(Guid id)
        {
            var find = await _context.Conquests.FirstOrDefaultAsync(x => x.Id == id);

            if (find == null)
            {
                return ResponseModel.BuildConflictResponse("Conquista não encontrada no banco de dados!");
            }

            _context.Remove(find);
            await _context.SaveChangesAsync();

            return ResponseModel.BuildOkResponse("Conquista removida com sucesso!");
        }
    }
}
