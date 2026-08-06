using System.Collections.Generic;
using System.Threading.Tasks;
using WorkflowPro.Models;

namespace WorkflowPro.Repositories
{
    /// <summary>
    /// Specific repository interface for Employee entity domain operations.
    /// </summary>
    public interface IEmployeeRepository : IRepository<Employee>
    {
        IEnumerable<Employee> GetEmployeesByDepartment(int departmentId);
        Task<IEnumerable<Employee>> GetEmployeesByDepartmentAsync(int departmentId);
        IEnumerable<Employee> SearchEmployees(string searchTerm, int? departmentId, bool? isActive);
        Employee GetEmployeeByCode(string employeeCode);
        Task<Employee> GetEmployeeByCodeAsync(string employeeCode);
        Employee GetEmployeeByEmail(string email);
        IEnumerable<Employee> GetActiveEmployees();
    }
}

