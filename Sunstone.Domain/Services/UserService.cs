using Microsoft.EntityFrameworkCore;
using Sunstone.Domain.Dtos;
using Sunstone.Domain.Helpers;
using Sunstone.Persistance;
using Sunstone.Persistance.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sunstone.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly SunstoneDbContext _dbContext;

        public UserService(SunstoneDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUser(int id)
        {
            return await _dbContext.User.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<User>> GetUsers()
        {
            return await _dbContext.User.AsNoTracking().ToListAsync();
        }

        public async Task<User> CreateUser(UserDto userToCreate)
        {
            if (!ValidationHelper.IsValidEmail(userToCreate.Email))
            {
                throw new Exception("Inavlid Email.");
            }

            if (!ValidationHelper.IsValidAge(userToCreate.DateOfBirth))
            {
                throw new Exception("Inavlid Age: User must be 18yrs or older.");
            }

            try
            {
                User user = new User()
                {
                    FirstName = userToCreate.FirstName,
                    LastName = userToCreate.LastName,
                    DateOfBirth = userToCreate.DateOfBirth,
                    Email = userToCreate.Email
                };

                await _dbContext.User.AddAsync(user);
                await _dbContext.SaveChangesAsync();

                return user;
            }
            catch (Exception)
            {
                throw new Exception($"Exception occured while creating user.");
            }
        }

        public async Task DeleteUser(int id)
        {
            var user = await _dbContext.User.FirstOrDefaultAsync(u => u.Id == id);

            if (user != null)
            {
                _dbContext.User.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Inavlid user Id.");
            }
        }

        public async Task<User> UpdateUser(UserDto userToUpdate)
        {
            if (!ValidationHelper.IsValidEmail(userToUpdate.Email))
            {
                throw new Exception("Inavlid Email.");
            }

            if (!ValidationHelper.IsValidAge(userToUpdate.DateOfBirth))
            {
                throw new Exception("Inavlid Age: User must be 18yrs or older.");
            }

            var user = await _dbContext.User.FirstOrDefaultAsync(u => u.Id == userToUpdate.Id);

            if (user != null)
            {
                try
                {
                    user.FirstName = userToUpdate.FirstName;
                    user.LastName = userToUpdate.LastName;
                    user.Email = userToUpdate.Email;
                    user.DateOfBirth = userToUpdate.DateOfBirth;

                    await _dbContext.SaveChangesAsync();
                    return user;
                }
                catch (Exception)
                {
                    throw new Exception($"Exception occured while updating user.");
                }
            }
            else
            {
                throw new Exception("Inavlid user Id: Couldn't find user.");
            }
        }
    }
}
