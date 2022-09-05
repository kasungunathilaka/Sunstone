using Sunstone.Domain.Dtos;
using Sunstone.Persistance.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sunstone.Domain.Services
{
    public interface IUserService
    {
        Task<List<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<User> CreateUser(UserDto userToCreate);
        Task<User> UpdateUser(UserDto userToUpdate);
        Task DeleteUser(int id);
    }
}
