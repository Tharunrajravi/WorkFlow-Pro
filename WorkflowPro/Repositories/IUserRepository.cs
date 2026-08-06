using System.Collections.Generic;
using System.Threading.Tasks;
using WorkflowPro.Models;

namespace WorkflowPro.Repositories
{
    /// <summary>
    /// Specific repository interface for User entity operations.
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
        User GetUserByUsername(string username);
        Task<User> GetUserByUsernameAsync(string username);
        User GetUserByEmail(string email);
        IEnumerable<User> GetActiveUsers();
        IEnumerable<User> GetUsersByRole(string role);
    }
}

