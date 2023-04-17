using AspNetCore.IQueryable.Extensions.Filter;
using Gamification.App.Extensions;
using Gamification.App.Models;
using Gamification.App.Services.Interfaces;
using Gamification.Application.Extensions;
using Gamification.Core.Entities;
using Gamification.Infra.Context;
using keener.Models;
using Microsoft.EntityFrameworkCore;
using static Gamification.App.Models.OrderModels;

namespace Gamification.App.Services
{
    public class OrderService : IOrderService
    {
        private AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel> GetOne(Guid id)
        {
            var sector = await _context.Orders.Where(x => x.Id == id).AsNoTracking().MapToDTO().FirstOrDefaultAsync();
            return ResponseModel.BuildOkResponse(sector);
        }

        public async Task<ResponseModel> List()
        {
            var orders = await _context.Orders.MapToDTO().ToListAsync();
            return ResponseModel.BuildOkResponse(orders);
        }

        public async Task<ResponseModel> ListPaginate(OrderFilterModel filter)
        {
            return ResponseModel.BuildOkResponse(await _context.Orders.Filter(filter).AsNoTracking().Include(x => x.Sector).MapToDTO().ReturnPaginated(filter.Page));
        }

        public async Task<ResponseModel> AddAsync(OrderAddModel model)
        {
            var find = await _context.Orders.FirstOrDefaultAsync(x => x.Title == model.Title);

            if (find != null)
            {
                return ResponseModel.BuildConflictResponse("Já existe um pedido com esse título!");
            }

            await _context.AddAsync(new Order
            {
                Title = model.Title,
                Description = model.Description,
                Step = model.Step,
                SectorId = model.SectorId,
            });
            await _context.SaveChangesAsync();

            return ResponseModel.BuildOkResponse("Pedido cadastrado com sucesso!");
        }

        public async Task<ResponseModel> EditAsync(OrderEditModel model)
        {
            var find = await _context.Orders.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (find == null)
            {
                return ResponseModel.BuildConflictResponse("Pedido não encontrado no banco de dados!");
            }

            find.Title = model.Title;
            find.Description = model.Description;
            find.RewardPoints = model.RewardPoints;
            find.SectorId = model.SectorId;

            _context.Update(find);
            await _context.SaveChangesAsync();

            return ResponseModel.BuildOkResponse("Pedido atualizado com sucesso!");
        }

        public async Task<ResponseModel> RemoveAsync(Guid id)
        {
            var find = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);

            if (find == null)
            {
                return ResponseModel.BuildConflictResponse("Pedido não encontrado no banco de dados!");
            }

            _context.Remove(find);
            await _context.SaveChangesAsync();

            return ResponseModel.BuildOkResponse("Pedido removido com sucesso!");
        }
    }
}
