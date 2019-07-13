using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.ViewModels;

namespace WebApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserVM> RegisterAsync(string username, string password);

        Task<UserVM> AuthenticateAsync(string username, string password);

        Task<UserVM> GetByIdAsync(int id);

        Task<List<UserVM>> GetAllAsync();
    }
}
