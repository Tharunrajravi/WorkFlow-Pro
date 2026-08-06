using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WorkflowPro.Common;
using WorkflowPro.Models;

namespace WorkflowPro.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(EmployeeDBContext context) : base(context) { }

        public Department GetDepartmentByCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code)) return null;

            try
            {
                string cleanCode = code.Trim().ToLower();
                return DbSet.FirstOrDefault(d => d.Code.ToLower() == cleanCode);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving department by code.", ex);
            }
        }

        public async Task<Department> GetDepartmentByCodeAsync(string code)
        {
            if (string.IsNullOrWhiteSpace(code)) return null;

            try
            {
                string cleanCode = code.Trim().ToLower();
                return await DbSet.FirstOrDefaultAsync(d => d.Code.ToLower() == cleanCode);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving department by code asynchronously.", ex);
            }
        }

        public IEnumerable<Department> GetActiveDepartments()
        {
            try
            {
                return DbSet
                    .Include(d => d.Employees)
                    .Where(d => d.IsActive)
                    .OrderBy(d => d.DepartmentName)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving active departments.", ex);
            }
        }
    }
}

