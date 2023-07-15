using AspNetCore.IQueryable.Extensions.Filter;
using Gamification.App.Extensions;
using Gamification.App.Models;
using Gamification.App.Services.Interfaces;
using Gamification.Application.Extensions;
using Gamification.Core.DTOs;
using Gamification.Core.Entities;
using Gamification.Infra.Context;
using Gamification.WebSocket.Handlers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using static Gamification.App.Models.OrderModels;

namespace Gamification.App.Services
{
    public class OrderService : IOrderService
    {
        private AppDbContext _context;
        private readonly RoomHandler _ws;

        public OrderService(AppDbContext context, RoomHandler ws)
        {
            _context = context;
            _ws = ws;
        }

        public async Task<ResponseModel> GetNotConcludedServices(Guid sectorId)
        {
            var orders = await _context.Orders.Where(x => x.SectorId == sectorId && x.Step != Step.Done).ToListAsync();
            return ResponseModel.BuildOkResponse(new { orders.Count, Points = orders.Sum(x => x.RewardPoints) });
        }

        public async Task<ResponseModel> CheckConquest(string userId)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                return ResponseModel.BuildConflictResponse("Usuário não encontrado");
            }

            if (user.Type == UserType.Standard)
            {
                var standard = await _context.StandardUsers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId);
                var sector = await _context.Sectors.AsNoTracking().Include(x => x.Users).Include(x => x.Orders).FirstOrDefaultAsync(x => x.Id == standard!.SectorId);

                var conquests = await _context.Conquests.AsNoTracking().ToListAsync();

                var userConquests = await _context.UserConquests.Where(x => x.UserId == userId).ToListAsync();
                var sectorConquests = await _context.SectorConquests.Where(x => x.SectorId == sector!.Id).ToListAsync();

                var conquestAdded = new List<ConquestDTO>();

                foreach (var conquest in conquests)
                {
                    if (conquest.EndDate == null || conquest.EndDate.Value.Date >= DateTime.Now.Date)
                    {
                        if (conquest.To == ConquestTo.User)
                        {
                            if (!userConquests.Select(x => x.ConquestId).Contains(conquest.Id))
                            {
                                var conq = new UserConquest
                                {
                                    UserId = userId,
                                    ConquestId = conquest.Id,
                                };

                                if (conquest.Type == ConquestType.Points && conquest.Points <= standard!.Points)
                                {
                                    await _context.UserConquests.AddAsync(conq);
                                    conquestAdded.Add(new ConquestDTO(conquest));
                                }

                                if (conquest.Type == ConquestType.ServicesConcluded && conquest.ServicesConcludedCount <= standard!.ConcludedOrders)
                                {
                                    await _context.UserConquests.AddAsync(conq);
                                    conquestAdded.Add(new ConquestDTO(conquest));
                                }
                            }
                        }

                        if (conquest.To == ConquestTo.Sector)
                        {
                            if (!sectorConquests.Select(x => x.ConquestId).Contains(conquest.Id))
                            {
                                var conq = new SectorConquest
                                {
                                    SectorId = sector!.Id,
                                    ConquestId = conquest.Id,
                                };

                                if (conquest.Type == ConquestType.Points && conquest.Points <= sector!.Points)
                                {
                                    await _context.SectorConquests.AddAsync(conq);
                                    conquestAdded.Add(new ConquestDTO(conquest));
                                }

                                if (conquest.Type == ConquestType.ServicesConcluded && conquest.ServicesConcludedCount <= sector!.ConcludedOrders)
                                {
                                    await _context.SectorConquests.AddAsync(conq);
                                    conquestAdded.Add(new ConquestDTO(conquest));
                                }
                            }
                        }
                    }
                }

                await _context.SaveChangesAsync();

                string wsResponse = JsonConvert.SerializeObject(new { userId = user.Id, Conquests = conquestAdded }, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                await _ws.SendMessageToGroup(user.Id, wsResponse);
            }

            if (user.Type == UserType.Supervisor)
            {
                var supervisor = await _context.SupervisorUsers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId);
                var sector = await _context.Sectors.AsNoTracking().Include(x => x.Users).Include(x => x.Orders).FirstOrDefaultAsync(x => x.SupervisorId == supervisor!.Id);

                var conqQuery = _context.Conquests.Where(x => x.Points <= supervisor!.Points || x.ServicesConcludedCount <= supervisor!.ConcludedOrders).AsNoTracking();
                var sectorConquests = new List<SectorConquest>();

                if (sector == null)
                {
                    conqQuery = conqQuery.Where(x => x.Points <= sector!.Points || x.ServicesConcludedCount <= sector!.ConcludedOrders);
                    sectorConquests = await _context.SectorConquests.Where(x => x.SectorId == sector!.Id).ToListAsync();
                }

                var conquests = await conqQuery.ToListAsync();

                var userConquests = await _context.UserConquests.Where(x => x.UserId == userId).ToListAsync();

                var conquestAdded = new List<ConquestDTO>();

                foreach (var conquest in conquests)
                {
                    if (conquest.EndDate == null || conquest.EndDate.Value.Date <= DateTime.Now.Date)
                    {
                        if (conquest.To == ConquestTo.User)
                        {
                            if (!userConquests.Select(x => x.ConquestId).Contains(conquest.Id))
                            {
                                var conq = new UserConquest
                                {
                                    UserId = userId,
                                    ConquestId = conquest.Id,
                                };
                                await _context.UserConquests.AddAsync(conq);
                                conquestAdded.Add(new ConquestDTO(conquest));
                            }
                        }

                        if (conquest.To == ConquestTo.Sector)
                        {

                            if (!sectorConquests.Select(x => x.ConquestId).Contains(conquest.Id))
                            {
                                await _context.SectorConquests.AddAsync(new SectorConquest
                                {
                                    SectorId = sector!.Id,
                                    ConquestId = conquest.Id,
                                });
                                conquestAdded.Add(new ConquestDTO(conquest));
                            }
                        }
                    }
                }

                string wsResponse = JsonConvert.SerializeObject(new { userId = user.Id, Conquests = conquestAdded }, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                await _ws.SendMessageToGroup(user.Id, wsResponse);
            }

            return ResponseModel.BuildOkResponse("Conquistas revisadas");
        }

        public async Task<ResponseModel> UpdateStep(OrderStepUpdate model, string userId)
        {
            var find = await _context.Orders.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (find == null)
            {
                return ResponseModel.BuildConflictResponse("Pedido não encontrado no banco de dados!");
            }

            find.Step = model.Step;

            _context.Update(find);
            await _context.SaveChangesAsync();

            if (find.Step == Step.Done)
            {
                var findUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
                if (findUser != null)
                {
                    findUser.Points += find.RewardPoints;
                    findUser.ConcludedOrders++;

                    _context.Update(findUser);
                    await _context.SaveChangesAsync();
                    await CheckConquest(findUser.Id);
                }
            }


            return ResponseModel.BuildOkResponse("Pedido atualizado com sucesso!");
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
            return ResponseModel.BuildOkResponse(await _context.Orders.OrderBy(x => x.Step).ApplyFilter(filter).AsNoTracking().Include(x => x.Sector).MapToDTO().ReturnPaginated(filter.Page));
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
                RewardPoints = model.RewardPoints,
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
