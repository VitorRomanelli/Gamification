using Gamification.App.Models;
using Gamification.App.Services.Interfaces;
using Gamification.Core.DTOs;
using Gamification.Core.Entities;
using Gamification.Infra.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Gamification.App.Services
{
    public class AuthService : IAuthService
    {
        public readonly SignInManager<User> _manager;
        private readonly UserManager<User> _userManager;
        public IConfiguration _configuration;
        public readonly AppDbContext _context;

        public AuthService(SignInManager<User> manager, UserManager<User> userManager, IConfiguration configuration, AppDbContext context)
        {
            _manager = manager;
            _configuration = configuration;
            _context = context;
            _userManager = userManager;
        }

        public SecurityToken GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("TokenAuthentication")["SecretKey"]!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim(ClaimTypes.Authentication, user.Id)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration.GetSection("TokenAuthentication")["Issuer"],
                Audience = _configuration.GetSection("TokenAuthentication")["Audience"]
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return token;
        }

        public async Task<ResponseModel> LoginAsync(LoginModel model)
        {
            var findUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == model.Email);

            if (findUser is null)
            {
                return ResponseModel.BuildUnauthorizedResponse("Usuário ou senha incorretos");
            }

            if (findUser.Status == UserStatus.Inactive)
            {
                return ResponseModel.BuildUnauthorizedResponse("Usuário desativado");
            }

            var result = await _manager.PasswordSignInAsync(findUser, model.Password, false, lockoutOnFailure: true);
            if (!result.Succeeded)
            {
                return ResponseModel.BuildUnauthorizedResponse("Usuário ou senha incorretos");
            }

            if (findUser.Type == UserType.Standard)
            {
                var user = await _context.StandardUsers.FirstOrDefaultAsync(x => x.Id == findUser.Id);
                var defaultToken = GenerateToken(user);
                return ResponseModel.BuildOkResponse(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(defaultToken),
                    validTo = defaultToken.ValidTo,
                    user = new UserDTO(user),
                });
            }
            else
            {
                var defaultToken = GenerateToken(findUser);
                return ResponseModel.BuildOkResponse(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(defaultToken),
                    validTo = defaultToken.ValidTo,
                    user = new UserDTO(findUser),
                });
            }
        }
    }
}
