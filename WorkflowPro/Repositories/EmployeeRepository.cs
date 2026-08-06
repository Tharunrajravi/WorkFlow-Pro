using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WorkflowPro.Common;
using WorkflowPro.Models;

namespace WorkflowPro.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EmployeeDBContext context) : base(context) { }

        public IEnumerable<Employee> GetEmployeesByDepartment(int departmentId)
        {
            try
            {
                return DbSet
                    .Include(e => e.Department)
                    .Where(e => e.DepartmentId == departmentId)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving employees by department.", ex);
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByDepartmentAsync(int departmentId)
        {
            try
            {
                return await DbSet
                    .Include(e => e.Department)
                    .Where(e => e.DepartmentId == departmentId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving employees by department asynchronously.", ex);
            }
        }

        public IEnumerable<Employee> SearchEmployees(string searchTerm, int? departmentId, bool? isActive)
        {
            try
            {
                var query = DbSet.Include(e => e.Department).AsQueryable();

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    string term = searchTerm.Trim().ToLower();
                    query = query.Where(e =>
                        e.FirstName.ToLower().Contains(term) ||
                        e.LastName.ToLower().Contains(term) ||
                        e.EmployeeCode.ToLower().Contains(term) ||
                        e.Email.ToLower().Contains(term) ||
                        e.Designation.ToLower().Contains(term));
                }

                if (departmentId.HasValue && departmentId.Value > 0)
                {
                    query = query.Where(e => e.DepartmentId == departmentId.Value);
                }

                if (isActive.HasValue)
                {
                    query = query.Where(e => e.IsActive == isActive.Value);
                }

                return query.OrderBy(e => e.EmployeeCode).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error searching employees.", ex);
            }
        }

        public Employee GetEmployeeByCode(string employeeCode)
        {
            if (string.IsNullOrWhiteSpace(employeeCode)) return null;

            try
            {
                string code = employeeCode.Trim().ToLower();
                return DbSet
                    .Include(e => e.Department)
                    .FirstOrDefault(e => e.EmployeeCode.ToLower() == code);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving employee by code.", ex);
            }
        }

        public async Task<Employee> GetEmployeeByCodeAsync(string employeeCode)
        {
            if (string.IsNullOrWhiteSpace(employeeCode)) return null;

            try
            {
                string code = employeeCode.Trim().ToLower();
                return await DbSet
                    .Include(e => e.Department)
                    .FirstOrDefaultAsync(e => e.EmployeeCode.ToLower() == code);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving employee by code asynchronously.", ex);
            }
        }

        public Employee GetEmployeeByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return null;

            try
            {
                string cleanEmail = email.Trim().ToLower();
                return DbSet.FirstOrDefault(e => e.Email.ToLower() == cleanEmail);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving employee by email.", ex);
            }
        }

        public IEnumerable<Employee> GetActiveEmployees()
        {
            try
            {
                return DbSet
                    .Include(e => e.Department)
                    .Where(e => e.IsActive)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving active employees.", ex);
            }
        }
    }
}

