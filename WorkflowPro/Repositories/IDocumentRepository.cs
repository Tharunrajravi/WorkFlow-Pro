using System.Collections.Generic;
using System.Threading.Tasks;
using WorkflowPro.Models;

namespace WorkflowPro.Repositories
{
    /// <summary>
    /// Specific repository interface for Document entity operations.
    /// </summary>
    public interface IDocumentRepository : IRepository<Document>
    {
        IEnumerable<Document> GetDocumentsForEmployee(int employeeId);
        Task<IEnumerable<Document>> GetDocumentsForEmployeeAsync(int employeeId);
        IEnumerable<Document> GetDocumentsForProject(int projectId);
        Task<IEnumerable<Document>> GetDocumentsForProjectAsync(int projectId);
        IEnumerable<Document> GetDocumentsByType(string documentType);
    }
}

