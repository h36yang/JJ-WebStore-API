using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApi.DataAccess.Entities;
using WebApi.DataAccess.Extensions;
using WebApi.DataAccess.Repositories.Interfaces;
using WebApi.Helpers;
using WebApi.Services.Interfaces;
using WebApi.ViewModels;

namespace WebApi.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IOptions<AppSettings> appSettings, IUserRepository userRepository, IMapper mapper)
        {
            _appSettings = appSettings.Value;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserVM> RegisterAsync(UserVM user)
        {
            if (await _userRepository.ExistAsync(x => x.Username == user.Username))
            {
                return null;
            }

            user.IsAdmin = false; // Doesn't matter what gets passed in, we don't allow creating Admin user here
            user.IsActive = true; // Doesn't matter what gets passed in, new user should be active
            await _userRepository.AddAsync(_mapper.Map<User>(user));
            return await AuthenticateAsync(user.Username, user.Password);
        }

        public async Task<UserVM> AuthenticateAsync(string username, string password)
        {
            User dbUser = await _userRepository.SingleOrDefaultAsync(x => x.IsActive && x.Username == username && x.Password == password);

            // return null if user not found
            if (dbUser.IsObjectNull())
            {
                return null;
            }

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, dbUser.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            UserVM user = _mapper.Map<UserVM>(dbUser);
            user.Token = tokenHandler.WriteToken(token);
            user.Password = null; // remove password before returning
            return user;
        }

        public async Task<UserVM> GetByIdAsync(int id)
        {
            User dbUser = await _userRepository.GetAsync(id);
            if (dbUser.IsObjectNull())
            {
                return null;
            }
            dbUser.Password = null; // remove password before returning
            return _mapper.Map<UserVM>(dbUser);
        }

        public async Task<List<UserVM>> GetAllAsync()
        {
            List<User> dbUsers = await _userRepository.GetAllAsync();
            return dbUsers.Select(x =>
            {
                x.Password = null; // remove password before returning
                return _mapper.Map<UserVM>(x);
            }).ToList();
        }
    }
}
