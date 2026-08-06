using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WorkflowPro.Common;
using WorkflowPro.Models;

namespace WorkflowPro.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(EmployeeDBContext context) : base(context) { }

        public IEnumerable<Project> GetActiveProjects()
        {
            try
            {
                return DbSet
                    .Include(p => p.Department)
                    .Where(p => p.Status.ToLower() != "completed")
                    .OrderBy(p => p.ProjectName)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving active projects.", ex);
            }
        }

        public async Task<IEnumerable<Project>> GetActiveProjectsAsync()
        {
            try
            {
                return await DbSet
                    .Include(p => p.Department)
                    .Where(p => p.Status.ToLower() != "completed")
                    .OrderBy(p => p.ProjectName)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving active projects asynchronously.", ex);
            }
        }

        public IEnumerable<Project> GetProjectsByDepartment(int departmentId)
        {
            try
            {
                return DbSet
                    .Include(p => p.Department)
                    .Where(p => p.DepartmentId == departmentId)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving projects by department.", ex);
            }
        }

        public Project GetProjectByCode(string projectCode)
        {
            if (string.IsNullOrWhiteSpace(projectCode)) return null;

            try
            {
                string cleanCode = projectCode.Trim().ToLower();
                return DbSet
                    .Include(p => p.Department)
                    .FirstOrDefault(p => p.ProjectCode.ToLower() == cleanCode);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving project by code.", ex);
            }
        }

        public async Task<Project> GetProjectByCodeAsync(string projectCode)
        {
            if (string.IsNullOrWhiteSpace(projectCode)) return null;

            try
            {
                string cleanCode = projectCode.Trim().ToLower();
                return await DbSet
                    .Include(p => p.Department)
                    .FirstOrDefaultAsync(p => p.ProjectCode.ToLower() == cleanCode);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving project by code asynchronously.", ex);
            }
        }

        public IEnumerable<Project> GetProjectsByStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status)) return new List<Project>();

            try
            {
                string cleanStatus = status.Trim().ToLower();
                return DbSet
                    .Include(p => p.Department)
                    .Where(p => p.Status.ToLower() == cleanStatus)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving projects by status.", ex);
            }
        }
    }
}

