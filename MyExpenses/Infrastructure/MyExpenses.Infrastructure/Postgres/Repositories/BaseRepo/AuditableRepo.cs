using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Entities.Base;
using MyExpenses.Domain.core.Repositories.Base;
using System.Linq.Expressions;

namespace MyExpenses.Infrastructure.Postgres.Repositories.BaseRepo
{
    /// <summary>
    /// Provides a generic repository implementation for entities that inherit from <see cref="AuditableEntity"/>.
    /// Supports basic CRUD operations with auditing functionality.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    public class AuditableRepo<T> : IAuditableRepo<T> where T : AuditableEntity
    {
        protected readonly MyExpensesDbContext _dbContext;
        protected readonly IAuthHelperContract _authHelper;
        protected readonly DbSet<T> _dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditableRepo{T}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context used for accessing the database.</param>
        /// <param name="authHelper">The authentication helper used for retrieving user information.</param>
        public AuditableRepo(MyExpensesDbContext dbContext, IAuthHelperContract authHelper)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
            _authHelper = authHelper;
        }

        /// <summary>
        /// Asynchronously creates a new entity in the database.
        /// </summary>
        /// <param name="entity">The entity to be created.</param>
        /// <returns>True if the entity was successfully created; otherwise, false.</returns>
        public async Task<bool> CreateAsync(T entity)
        {
            try
            {
                entity = SetCreateAuditFields(entity);
                var result = await _dbSet.AddAsync(entity);
                var res = await _dbContext.SaveChangesAsync();
                return res > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Asynchronously retrieves all entities from the database.
        /// </summary>
        /// <returns>An enumerable list of all entities.</returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        /// <summary>
        /// Asynchronously retrieves an entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the entity to retrieve.</param>
        /// <returns>The entity with the specified ID.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the entity with the specified ID is not found.</exception>
        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Entity with id {id} not found.");
            return entity;
        }

        /// <summary>
        /// Asynchronously updates entities in the database that match the specified filter expression.
        /// </summary>
        /// <param name="filterExpression">The filter expression to identify the entities to be updated.</param>
        /// <param name="updateExpression">The update expression defining the properties to update.</param>
        /// <returns>True if at least one entity was successfully updated; otherwise, false.</returns>
        public async Task<bool> UpdateAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> updateExpression)
        {
            var entity = _dbSet.Where(filterExpression);
            var updatedRows = await entity.ExecuteUpdateAsync(updateExpression);
            if (updatedRows == 0)
                return false;
            updatedRows = await entity.ExecuteUpdateAsync(e => e
                                .SetProperty(e => e.UpdatedOn, DateTime.UtcNow)
                                .SetProperty(e => e.UpdatedBy, _authHelper.GetCurrentUserId()));
            return updatedRows > 0;
        }

        /// <summary>
        /// Asynchronously deletes an entity from the database.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>The deleted entity.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the entity is not found.</exception>
        public async Task<T> DeleteAsync(T entity)
        {
            if (entity == null)
                throw new KeyNotFoundException($"Entity not found.");
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Asynchronously deletes an entity from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the entity to delete.</param>
        /// <returns>Return True if the entity with given id deleted successfully.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the entity with the specified ID is not found.</exception>
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Entity with id {id} not found.");
           var res= await DeleteAsync(entity);
            return res != null;
        }

        /// <summary>
        /// Searches for entities in the database that match the specified expression.
        /// </summary>
        /// <param name="expression">The expression to filter entities.</param>
        /// <returns>An <see cref="IQueryable{T}"/> representing the entities that match the filter.</returns>
        public IQueryable<T> Search(Expression<Func<T, bool>> expression)
        {
            var entities = _dbSet.Where(expression);
            return entities;
        }

        /// <summary>
        /// Sets the audit fields for entity creation.
        /// </summary>
        /// <param name="entity">The entity for which to set audit fields.</param>
        /// <returns>The entity with updated audit fields.</returns>
        private T SetCreateAuditFields(T entity)
        {
            entity.CreatedOn = DateTime.UtcNow;
            entity.CreatedBy = _authHelper.GetCurrentUserId();
            return entity;
        }

        /// <summary>
        /// Sets the audit fields for entity updates.
        /// </summary>
        /// <param name="entity">The entity for which to set audit fields.</param>
        /// <returns>The entity with updated audit fields.</returns>
        private T SetUpdateAuditFields(T entity)
        {
            entity.UpdatedOn = DateTime.UtcNow;
            entity.UpdatedBy = _authHelper.GetCurrentUserId();
            return entity;
        }
    }
}
