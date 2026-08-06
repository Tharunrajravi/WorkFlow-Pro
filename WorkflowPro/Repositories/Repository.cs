using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WorkflowPro.Common;
using WorkflowPro.Models;

namespace WorkflowPro.Repositories
{
    /// <summary>
    /// Generic Entity Framework repository implementation of IRepository interface.
    /// </summary>
    /// <typeparam name="T">Entity class type.</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly EmployeeDBContext Context;
        protected readonly DbSet<T> DbSet;

        public Repository(EmployeeDBContext context)
        {
            Context = context ?? throw new ArgumentNullException("context");
            DbSet = Context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            try
            {
                return DbSet.ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Failed to retrieve entity list.", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await DbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Failed to retrieve entity list asynchronously.", ex);
            }
        }

        public virtual T GetById(int id)
        {
            try
            {
                return DbSet.Find(id);
            }
            catch (Exception ex)
            {
                throw new RepositoryException(string.Format("Failed to retrieve entity with ID {0}.", id), ex);
            }
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            try
            {
                return await DbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new RepositoryException(string.Format("Failed to retrieve entity asynchronously with ID {0}.", id), ex);
            }
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return DbSet.Where(predicate).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Failed to query entities matching predicate.", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await DbSet.Where(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Failed to query entities matching predicate asynchronously.", ex);
            }
        }

        public virtual void Add(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            try
            {
                DbSet.Add(entity);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Failed to add entity to database context.", ex);
            }
        }

        public virtual void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            try
            {
                Context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Failed to update entity state in database context.", ex);
            }
        }

        public virtual void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            try
            {
                if (Context.Entry(entity).State == EntityState.Detached)
                {
                    DbSet.Attach(entity);
                }
                DbSet.Remove(entity);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Failed to remove entity from database context.", ex);
            }
        }

        public virtual void DeleteById(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        public virtual int SaveChanges()
        {
            try
            {
                return Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Failed to persist changes to the database.", ex);
            }
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            try
            {
                return await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Failed to persist changes to the database asynchronously.", ex);
            }
        }
    }
}

