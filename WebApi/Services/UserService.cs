using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WebApi.Helpers;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IUserService
    {
        User Register(Models.Database.User user);
        User Authenticate(string username, string password);
        Models.Database.User GetById(int id);
        IEnumerable<Models.Database.User> GetAll();
    }

    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly Models.Database.WebStoreContext _context;

        public UserService(IOptions<AppSettings> appSettings, Models.Database.WebStoreContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public User Register(Models.Database.User user)
        {
            _context.User.Add(user);
            _context.SaveChanges();
            return Authenticate(user.Username, user.Password);
        }

        public User Authenticate(string username, string password)
        {
            List<Models.Database.User> dbUsers = _context.User.Where(x => x.IsActive.Value).ToList();
            var dbUser = dbUsers.SingleOrDefault(x => x.Username == username && x.Password == password);

            // return null if user not found
            if (dbUser == null)
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
            var user = new User(dbUser)
            {
                Token = tokenHandler.WriteToken(token),
                Password = null // remove password before returning
            };

            return user;
        }

        public Models.Database.User GetById(int id)
        {
            var user = _context.User.Find(id);
            if (user != null)
            {
                // remove password before returning
                user.Password = null;
            }
            return user;
        }

        public IEnumerable<Models.Database.User> GetAll()
        {
            List<Models.Database.User> users = _context.User.ToList();

            // return users without passwords
            return users.Select(x =>
            {
                x.Password = null;
                return x;
            });
        }
    }
}
