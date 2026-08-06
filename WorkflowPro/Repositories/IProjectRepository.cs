using System.Collections.Generic;
using System.Threading.Tasks;
using WorkflowPro.Models;

namespace WorkflowPro.Repositories
{
    /// <summary>
    /// Specific repository interface for Project entity operations.
    /// </summary>
    public interface IProjectRepository : IRepository<Project>
    {
        IEnumerable<Project> GetActiveProjects();
        Task<IEnumerable<Project>> GetActiveProjectsAsync();
        IEnumerable<Project> GetProjectsByDepartment(int departmentId);
        Project GetProjectByCode(string projectCode);
        Task<Project> GetProjectByCodeAsync(string projectCode);
        IEnumerable<Project> GetProjectsByStatus(string status);
    }
}

