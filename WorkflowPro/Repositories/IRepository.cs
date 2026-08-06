using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WorkflowPro.Repositories
{
    /// <summary>
    /// Generic Repository interface providing contract for standard CRUD operations and entity queries.
    /// </summary>
    /// <typeparam name="T">Entity type.</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Retrieves all records of entity type T.
        /// </summary>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Asynchronously retrieves all records of entity type T.
        /// </summary>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Retrieves entity by primary key identifier.
        /// </summary>
        T GetById(int id);

        /// <summary>
        /// Asynchronously retrieves entity by primary key identifier.
        /// </summary>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Finds entities matching a LINQ predicate condition.
        /// </summary>
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Asynchronously finds entities matching a LINQ predicate condition.
        /// </summary>
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Adds a new entity to database context.
        /// </summary>
        void Add(T entity);

        /// <summary>
        /// Updates existing entity in database context.
        /// </summary>
        void Update(T entity);

        /// <summary>
        /// Removes entity from database context.
        /// </summary>
        void Delete(T entity);

        /// <summary>
        /// Removes entity by primary key identifier.
        /// </summary>
        void DeleteById(int id);

        /// <summary>
        /// Persists all pending changes to database storage.
        /// </summary>
        int SaveChanges();

        /// <summary>
        /// Asynchronously persists all pending changes to database storage.
        /// </summary>
        Task<int> SaveChangesAsync();
    }
}

