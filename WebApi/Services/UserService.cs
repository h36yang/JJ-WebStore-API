﻿using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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

        public async Task<UserVM> RegisterAsync(string username, string password)
        {
            if (await _userRepository.ExistAsync(x => x.Username == username))
            {
                return null;
            }

            var dbUser = new User()
            {
                Username = username,
                Password = password
            };
            int result = await _userRepository.AddAsync(dbUser);
            if (result <= 0)
            {
                throw new ApplicationException("Failed to register user in the database");
            }
            return await AuthenticateAsync(dbUser.Username, dbUser.Password);
        }

        public async Task<UserVM> AuthenticateAsync(string username, string password)
        {
            User dbUser = await _userRepository.SingleOrDefaultAsync(x => x.IsActive.Value && x.Username == username && x.Password == password);

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

            UserVM user = _mapper.Map<User, UserVM>(dbUser);
            user.Token = tokenHandler.WriteToken(token);
            return user;
        }

        public async Task<UserVM> GetByIdAsync(int id)
        {
            User dbUser = await _userRepository.GetAsync(id);
            if (dbUser.IsObjectNull())
            {
                return null;
            }
            return _mapper.Map<User, UserVM>(dbUser);
        }

        public async Task<List<UserVM>> GetAllAsync()
        {
            List<User> dbUsers = await _userRepository.GetAllAsync();
            return _mapper.Map<List<User>, List<UserVM>>(dbUsers);
        }
    }
}
