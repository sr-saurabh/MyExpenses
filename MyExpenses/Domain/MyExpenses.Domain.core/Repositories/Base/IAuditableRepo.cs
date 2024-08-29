using Microsoft.EntityFrameworkCore.Query;
using MyExpenses.Domain.core.Entities.Base;
using System.Linq.Expressions;

namespace MyExpenses.Domain.core.Repositories.Base
{
    /// <summary>
    /// Defines the contract for a repository that handles auditable entities, providing basic CRUD operations with audit tracking.
    /// </summary>
    /// <typeparam name="T">The type of the auditable entity.</typeparam>
    public interface IAuditableRepo<T> where T : AuditableEntity
    {
        /// <summary>
        /// Asynchronously retrieves an entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the entity to retrieve.</param>
        /// <returns>The entity with the specified ID.</returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Asynchronously retrieves all entities from the database.
        /// </summary>
        /// <returns>An enumerable list of all entities.</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Asynchronously creates a new entity in the database.
        /// </summary>
        /// <param name="entity">The entity to be created.</param>
        /// <returns>True if the entity was successfully created; otherwise, false.</returns>
        Task<bool> CreateAsync(T entity);

        /// <summary>
        /// Asynchronously updates entities in the database that match the specified filter expression.
        /// </summary>
        /// <param name="filterExpression">The filter expression to identify the entities to be updated.</param>
        /// <param name="updateExpression">The update expression defining the properties to update.</param>
        /// <returns>True if at least one entity was successfully updated; otherwise, false.</returns>
        Task<bool> UpdateAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> updateExpression);

        /// <summary>
        /// Asynchronously deletes an entity from the database.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>The deleted entity.</returns>
        Task<T> DeleteAsync(T entity);

        /// <summary>
        /// Asynchronously deletes an entity from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the entity to delete.</param>
        /// <returns>Return True if the entity with given id deleted successfully.</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Searches for entities in the database that match the specified expression.
        /// </summary>
        /// <param name="expression">The expression to filter entities.</param>
        /// <returns>An <see cref="IQueryable{T}"/> representing the entities that match the filter.</returns>
        IQueryable<T> Search(Expression<Func<T, bool>> expression);
    }
}
