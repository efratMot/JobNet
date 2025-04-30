using JobNet.Core.Entities;
using JobNet.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JobNet.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.Where(s => !string.IsNullOrEmpty(s.UserName)).ToListAsync();
        }

        public User Get(int id)
        {
            try
            {
                return _context.Users.First(s => s.UserID == id);
            }
            catch
            {
                throw new Exception("User not found.");
            }
        }

        public User GetUserByLogin(string email, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public async Task<User> AddAsync(User user)
        {
            validateUser(user);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<User> DeleteAsync(int id)
        {
            User user = Get(id);
            if (user is null)
                throw new Exception("User not found.");
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateAsync(User user, int id)
        {
            User u = Get(id);
            if (u is null)
                throw new Exception("User not found.");
            validateUser(user);
            u.UserName = user.UserName;
            u.Password = user.Password;
            u.Role = user.Role;
            u.Email = user.Email;
            await _context.SaveChangesAsync();
            return u;
        }

        public void validateUser(User u)
        {
            if (string.IsNullOrEmpty(u.UserName) || u.UserName == "string")
                throw new ArgumentException("User name cannot be null or empty.");
            if (string.IsNullOrEmpty(u.Password) || u.Password == "string")
                throw new ArgumentException("Password cannot be null or empty.");
            if(u.Role<0)
                throw new ArgumentException("Role cannot be negative number.");
            if (u.Password.Length <= 3)
                throw new ArgumentException("Password must contain at least 4 chars.");

            if (string.IsNullOrEmpty(u.Email) || u.Email == "string")
                throw new ArgumentException("Email cannot be null or empty.");

            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(u.Email, emailPattern))
            {
                throw new ArgumentException("Email format is invalid.");
            }

            if (u.Email.Length > 254)
            {
                throw new ArgumentException("Email is too long. Maximum length is 254 characters.");
            }

            var domain = u.Email.Split('@')[1];
            if (!DomainExists(domain))
            {
                throw new ArgumentException("Email domain does not exist.");
            }
        }

        // פונקציה לבדוק קיום דומיין
        private bool DomainExists(string domain)
        {
            try
            {
                var host = Dns.GetHostEntry(domain);
                return true;
            }
            catch (SocketException)
            {
                return false;
            }
        }
    }
}
