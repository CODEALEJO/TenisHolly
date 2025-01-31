using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenisHolly.DTO;
using TenisHolly.Data;
using TenisHolly.Interface;
using TenisHolly.Models;
using Microsoft.EntityFrameworkCore;

namespace TenisHolly.Services
{
    public class UserServices : IUserInterface
    {
        private readonly ApplicationDbContext _context;

        public UserServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserDto> Add(UserDto userDto)
        {
            var user = new User
            {
                FullName = userDto.FullName,
                Email = userDto.Email,
                PasswordHash = userDto.PasswordHash,
                Role = userDto.Role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            userDto.Id = user.Id;
            return userDto;
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            return await _context.Users
                .Select(user => new UserDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    Role = user.Role
                })
                .ToListAsync();
        }

        public async Task<UserDto> GetById(int id)
        {
            return await _context.Users
                .Where(u => u.Id == id)
                .Select(user => new UserDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    Role = user.Role
                })
                .FirstOrDefaultAsync();
        }

        public async Task Update(UserDto userDto)
        {
            var user = await _context.Users.FindAsync(userDto.Id);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            user.FullName = userDto.FullName;
            user.Email = userDto.Email;
            user.PasswordHash = userDto.PasswordHash;
            user.Role = userDto.Role;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Register(UserDto userDto)
        {
            if (userDto == null)
            {
                throw new ArgumentNullException(nameof(userDto), "The user cannot be null.");
            }

            var user = new User
            {
                FullName = userDto.FullName,
                Email = userDto.Email,
                PasswordHash = userDto.PasswordHash,
                Role = userDto.Role
            };

            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                userDto.Id = user.Id;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error while adding the user to the database.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the user.", ex);
            }
        }
    }
}
