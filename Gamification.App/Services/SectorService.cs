using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Filter;
using AspNetCore.IQueryable.Extensions.Pagination;
using Gamification.App.Extensions;
using Gamification.App.Helpers;
using Gamification.App.Models;
using Gamification.App.Services.Interfaces;
using Gamification.Application.Extensions;
using Gamification.Core.Entities;
using Gamification.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamification.App.Services
{
    public class SectorService : ISectorService
    {
        public readonly AppDbContext _context;

        public SectorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel> GetOne(Guid id)
        {
            var sector = await _context.Sectors.Where(x => x.Id == id).AsNoTracking().Include(x => x.Users).MapToDTO().FirstOrDefaultAsync();
            return ResponseModel.BuildOkResponse(sector);
        }

        public async Task<ResponseModel> List()
        {
            var sectors = await _context.Sectors.MapToDTO().ToListAsync();
            return ResponseModel.BuildOkResponse(sectors);
        }

        public async Task<ResponseModel> ListPaginate(SectorFilterModel filter)
        {
            return ResponseModel.BuildOkResponse((await _context.Sectors.AsNoTracking().Include(x => x.Supervisor).Include(x => x.Users).MapToDTO().ToListAsync()).OrderByDescending(x => x.Points).ToList().ReturnPaginated(filter.Page));
        }

        public async Task<ResponseModel> AddAsync(SectorAddModel model)
        {
            var find = await _context.Sectors.FirstOrDefaultAsync(x => x.Name == model.Name || x.SupervisorId == model.SupervisorId);

            if (find != null)
            {
                return ResponseModel.BuildConflictResponse("Setor já cadastrado ou Supervisor já vinculado a outro setor!");
            }

            await _context.AddAsync(new Sector
            {
                Name = model.Name,
                Description = model.Description,
                SupervisorId = model.SupervisorId,
            });
            await _context.SaveChangesAsync();

            return ResponseModel.BuildOkResponse("Setor cadastrado com sucesso!");
        }

        public async Task<ResponseModel> EditAsync(SectorEditModel model)
        {
            var find = await _context.Sectors.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (find == null)
            {
                return ResponseModel.BuildConflictResponse("Setor não encontrado no banco de dados!");
            }

            find.Name = model.Name;
            find.Description = model.Description;
            find.SupervisorId = model.SupervisorId;

            _context.Update(find);
            await _context.SaveChangesAsync();

            return ResponseModel.BuildOkResponse("Setor atualizado com sucesso!");
        }

        public async Task<ResponseModel> RemoveAsync(Guid id)
        {
            var find = await _context.Sectors.FirstOrDefaultAsync(x => x.Id == id);

            if (find == null)
            {
                return ResponseModel.BuildConflictResponse("Setor não encontrado no banco de dados!");
            }

            _context.Remove(find);
            await _context.SaveChangesAsync();

            return ResponseModel.BuildOkResponse("Setor removido com sucesso!");
        }
    }
}
