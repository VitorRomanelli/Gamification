using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Filter;
using AspNetCore.IQueryable.Extensions.Pagination;
using Gamification.App.Extensions;
using Gamification.App.Helpers;
using Gamification.App.Models;
using Gamification.App.Services.Interfaces;
using Gamification.Application.Extensions;
using Gamification.Core.DTOs;
using Gamification.Core.Entities;
using Gamification.Infra.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Gamification.App.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _manager;
        public readonly AppDbContext _context;

        public UserService(AppDbContext context, UserManager<User> manager)
        {
            _context = context;
            _manager = manager;
        }

        public async Task<ResponseModel> ChangeUserStatusAsync(ChangeStatusModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (user == null)
            {
                return ResponseModel.BuildErrorResponse("Usuário não encontrado!");
            }

            user.Status = model.Status;

            _context.Update(user);
            await _context.SaveChangesAsync();

            return ResponseModel.BuildOkResponse("Status atualizado com sucesso!");
        }

        public async Task<ResponseModel> GetOne(string id)
        {
            var user = await _context.Users.Where(x => x.Id == id).MapToDTO().FirstOrDefaultAsync();

            if(user is null) 
            {
                return ResponseModel.BuildErrorResponse("Usuário não encontrado!");
            }
            
            if (user.Type == UserType.Standard) 
            {
                return ResponseModel.BuildOkResponse(await _context.StandardUsers.Where(x => x.Id == id).MapToDTO().FirstOrDefaultAsync());
            }

            return ResponseModel.BuildOkResponse(user);
        }

        public async Task<ResponseModel> List()
        {
            return ResponseModel.BuildOkResponse(await _context.Users.MapToDTO().ToListAsync());
        }

        public async Task<ResponseModel> ListSupervisor()
        {
            return ResponseModel.BuildOkResponse(await _context.Users.Where(x => x.Type == UserType.Supervisor).MapToDTO().ToListAsync());
        }

        public async Task<ResponseModel> GetUserMetrics(UserFilterModel filter)
        {
            var users = await _context.Users.Filter(filter).ToListAsync();

            var totalCount = users.Count;
            var activeCount = users.Count(x => x.Status == UserStatus.Active);
            var inactiveCount = users.Count(x => x.Status == UserStatus.Inactive);

            return ResponseModel.BuildOkResponse(new
            {
                Total = totalCount,
                Active = activeCount,
                Inactive = inactiveCount
            });
        }

        public async Task<ResponseModel> ListPaginate(UserFilterModel filter)
        {
            return ResponseModel.BuildOkResponse(await _context.Users.ApplyFilter(filter).OrderByDescending(x => x.Points).MapToDTO().ReturnPaginated(filter.Page));
        }

        public async Task<ResponseModel> AddAsync(UserAddModel user)
        {
            var userWithSameEmail = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email);

            if (userWithSameEmail != null)
            {
                return ResponseModel.BuildConflictResponse("Já existe um usuário com este e-mail no banco de dados!");
            }

            if (user.Type == UserType.Standard)
            {
                var result = await _manager.CreateAsync(new StandardUser { Name = user.Name, UserName = user.Email, Email = user.Email, PhoneNumber = user.Phone, Type = user.Type, SectorId = (Guid) user.SectorId! }, user.Password);

                if (result.Succeeded)
                {
                    return ResponseModel.BuildOkResponse("Usuário cadastrado com sucesso!");
                }
            }

            if (user.Type == UserType.Supervisor)
            {
                var result = await _manager.CreateAsync(new SupervisorUser { Name = user.Name, UserName = user.Email, Email = user.Email, PhoneNumber = user.Phone, Type = user.Type }, user.Password);

                if (result.Succeeded)
                {
                    return ResponseModel.BuildOkResponse("Usuário cadastrado com sucesso!");
                }
            }

            if (user.Type == UserType.Administrator)
            {
                var result = await _manager.CreateAsync(new AdministratorUser { Name = user.Name, UserName = user.Email, Email = user.Email, PhoneNumber = user.Phone, Type = user.Type }, user.Password);

                if (result.Succeeded)
                {
                    return ResponseModel.BuildOkResponse("Usuário cadastrado com sucesso!");
                }
            }

            return ResponseModel.BuildErrorResponse("Ocorreu um erro ao adicionar o usuário, verifique os dados e tente novamente!");
        }

        public async Task<ResponseModel> EditAsync(UserEditModel user)
        {
            if (user.Type == UserType.Standard)
            {
                var findedUser = await _context.StandardUsers.FirstOrDefaultAsync(x => x.Id == user.Id);

                if (findedUser == null)
                {
                    return ResponseModel.BuildResponse(404, "Usuário não encontrado");
                }

                findedUser.Name = user.Name;
                findedUser.Email = user.Email;
                findedUser.PhoneNumber = user.Phone;
                findedUser.SectorId = (Guid)user.SectorId!;

                if (!String.IsNullOrEmpty(user.Picture) && !user.Picture.Contains("Uploads"))
                {
                    findedUser.Picture = MediaHelper.SaveImage(user.Picture, $"Users/{findedUser.Id}", user.Extension!, $"{findedUser.Id}.{user.Extension}");
                }

                _context.Users.Update(findedUser);
                await _context.SaveChangesAsync();

                return ResponseModel.BuildOkResponse(new UserDTO(findedUser));
            }

            if (user.Type == UserType.Supervisor || user.Type == UserType.Administrator)
            {
                var findedUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);

                if (findedUser == null)
                {
                    return ResponseModel.BuildResponse(404, "Usuário não encontrado");
                }

                findedUser.Name = user.Name;
                findedUser.Email = user.Email;
                findedUser.PhoneNumber = user.Phone;

                if (!String.IsNullOrEmpty(user.Picture) && !user.Picture.Contains("Uploads"))
                {
                    findedUser.Picture = MediaHelper.SaveImage(user.Picture, $"Users/{findedUser.Id}", user.Extension!, $"{findedUser.Id}.{user.Extension}");
                }

                _context.Users.Update(findedUser);
                await _context.SaveChangesAsync();

                return ResponseModel.BuildOkResponse(new UserDTO(findedUser));
            }

            return ResponseModel.BuildErrorResponse("Ocorreu um erro");
        }

        public async Task<ResponseModel> RemoveAsync(string id)
        {
            var findedUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (findedUser == null)
            {
                return ResponseModel.BuildResponse(404, "Usuário não encontrado");
            }

            _context.Users.Remove(findedUser);
            await _context.SaveChangesAsync();

            return ResponseModel.BuildOkResponse("Usuário removido com sucesso!");
        }
    }
}
