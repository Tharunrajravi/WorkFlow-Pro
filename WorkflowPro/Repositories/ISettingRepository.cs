using System.Collections.Generic;
using System.Threading.Tasks;
using WorkflowPro.Models;

namespace WorkflowPro.Repositories
{
    /// <summary>
    /// Specific repository interface for Setting entity operations.
    /// </summary>
    public interface ISettingRepository : IRepository<Setting>
    {
        Setting GetByKey(string key);
        Task<Setting> GetByKeyAsync(string key);
        IEnumerable<Setting> GetByCategory(string category);
    }
}

