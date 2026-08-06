using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkflowPro.Models;

namespace WorkflowPro.Repositories
{
    /// <summary>
    /// Specific repository interface for AuditLog entity operations.
    /// </summary>
    public interface IAuditLogRepository : IRepository<AuditLog>
    {
        IEnumerable<AuditLog> GetLogsByUser(int userId);
        Task<IEnumerable<AuditLog>> GetLogsByUserAsync(int userId);
        IEnumerable<AuditLog> GetLogsByDateRange(DateTime fromDate, DateTime toDate);
        IEnumerable<AuditLog> GetLogsByEntity(string entityName, string entityId);
    }
}

