using System.Collections.Generic;
using System.Threading.Tasks;
using WorkflowPro.Models;

namespace WorkflowPro.Repositories
{
    /// <summary>
    /// Specific repository interface for Department entity operations.
    /// </summary>
    public interface IDepartmentRepository : IRepository<Department>
    {
        Department GetDepartmentByCode(string code);
        Task<Department> GetDepartmentByCodeAsync(string code);
        IEnumerable<Department> GetActiveDepartments();
    }
}

