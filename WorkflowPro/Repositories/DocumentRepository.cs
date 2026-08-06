using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WorkflowPro.Common;
using WorkflowPro.Models;

namespace WorkflowPro.Repositories
{
    public class DocumentRepository : Repository<Document>, IDocumentRepository
    {
        public DocumentRepository(EmployeeDBContext context) : base(context) { }

        public IEnumerable<Document> GetDocumentsForEmployee(int employeeId)
        {
            try
            {
                return DbSet
                    .Include(d => d.Employee)
                    .Include(d => d.Project)
                    .Where(d => d.EmployeeId == employeeId)
                    .OrderByDescending(d => d.UploadedDate)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving documents for employee.", ex);
            }
        }

        public async Task<IEnumerable<Document>> GetDocumentsForEmployeeAsync(int employeeId)
        {
            try
            {
                return await DbSet
                    .Include(d => d.Employee)
                    .Include(d => d.Project)
                    .Where(d => d.EmployeeId == employeeId)
                    .OrderByDescending(d => d.UploadedDate)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving documents for employee asynchronously.", ex);
            }
        }

        public IEnumerable<Document> GetDocumentsForProject(int projectId)
        {
            try
            {
                return DbSet
                    .Include(d => d.Employee)
                    .Include(d => d.Project)
                    .Where(d => d.ProjectId == projectId)
                    .OrderByDescending(d => d.UploadedDate)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving documents for project.", ex);
            }
        }

        public async Task<IEnumerable<Document>> GetDocumentsForProjectAsync(int projectId)
        {
            try
            {
                return await DbSet
                    .Include(d => d.Employee)
                    .Include(d => d.Project)
                    .Where(d => d.ProjectId == projectId)
                    .OrderByDescending(d => d.UploadedDate)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving documents for project asynchronously.", ex);
            }
        }

        public IEnumerable<Document> GetDocumentsByType(string documentType)
        {
            if (string.IsNullOrWhiteSpace(documentType)) return new List<Document>();

            try
            {
                string cleanType = documentType.Trim().ToLower();
                return DbSet
                    .Where(d => d.DocumentType.ToLower() == cleanType)
                    .OrderByDescending(d => d.UploadedDate)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving documents by type.", ex);
            }
        }
    }
}

