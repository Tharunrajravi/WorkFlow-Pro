using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WorkflowPro.Common;
using WorkflowPro.Models;

namespace WorkflowPro.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(EmployeeDBContext context) : base(context) { }

        public User GetUserByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) return null;

            try
            {
                return DbSet
                    .Include(u => u.Employee)
                    .FirstOrDefault(u => u.Username.Equals(username.Trim(), StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving user by username.", ex);
            }
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) return null;

            try
            {
                string cleanName = username.Trim().ToLower();
                return await DbSet
                    .Include(u => u.Employee)
                    .FirstOrDefaultAsync(u => u.Username.ToLower() == cleanName);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving user by username asynchronously.", ex);
            }
        }

        public User GetUserByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return null;

            try
            {
                string cleanEmail = email.Trim().ToLower();
                return DbSet.FirstOrDefault(u => u.Email.ToLower() == cleanEmail);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving user by email.", ex);
            }
        }

        public IEnumerable<User> GetActiveUsers()
        {
            try
            {
                return DbSet.Where(u => u.IsActive).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving active users list.", ex);
            }
        }

        public IEnumerable<User> GetUsersByRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role)) return new List<User>();

            try
            {
                string cleanRole = role.Trim().ToLower();
                return DbSet.Where(u => u.Role.ToLower() == cleanRole).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving users by role.", ex);
            }
        }
    }
}

