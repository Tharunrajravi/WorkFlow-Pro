using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WorkflowPro.Common;
using WorkflowPro.Models;

namespace WorkflowPro.Repositories
{
    public class AuditLogRepository : Repository<AuditLog>, IAuditLogRepository
    {
        public AuditLogRepository(EmployeeDBContext context) : base(context) { }

        public IEnumerable<AuditLog> GetLogsByUser(int userId)
        {
            try
            {
                return DbSet
                    .Include(a => a.User)
                    .Where(a => a.UserId == userId)
                    .OrderByDescending(a => a.Timestamp)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving audit logs by user.", ex);
            }
        }

        public async Task<IEnumerable<AuditLog>> GetLogsByUserAsync(int userId)
        {
            try
            {
                return await DbSet
                    .Include(a => a.User)
                    .Where(a => a.UserId == userId)
                    .OrderByDescending(a => a.Timestamp)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving audit logs by user asynchronously.", ex);
            }
        }

        public IEnumerable<AuditLog> GetLogsByDateRange(DateTime fromDate, DateTime toDate)
        {
            try
            {
                return DbSet
                    .Include(a => a.User)
                    .Where(a => a.Timestamp >= fromDate && a.Timestamp <= toDate)
                    .OrderByDescending(a => a.Timestamp)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving audit logs by date range.", ex);
            }
        }

        public IEnumerable<AuditLog> GetLogsByEntity(string entityName, string entityId)
        {
            if (string.IsNullOrWhiteSpace(entityName)) return new List<AuditLog>();

            try
            {
                string cleanEntity = entityName.Trim().ToLower();
                var query = DbSet.Include(a => a.User).Where(a => a.EntityName.ToLower() == cleanEntity);

                if (!string.IsNullOrWhiteSpace(entityId))
                {
                    string idVal = entityId.Trim();
                    query = query.Where(a => a.EntityId == idVal);
                }

                return query.OrderByDescending(a => a.Timestamp).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving audit logs by entity.", ex);
            }
        }
    }
}

