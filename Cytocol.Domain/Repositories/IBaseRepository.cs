using Cytocol.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Cytocol.Domain.Repositories
{
    /// <summary>
    /// Defines the base abstraction for all the repositories performing crud operations
    /// </summary>
    /// <typeparam name="TEntity">Type of the Entity taking part in CRUD.</typeparam>
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Method to persist the entity.
        /// </summary>
        /// <param name="entity">Entity to be saved</param>
        /// <returns>Entity which is successfully persisted.</returns>
        Task<TEntity> Save(TEntity entity);

        /// <summary>
        /// Method to modify the existing entity.
        /// </summary>
        /// <param name="entity">Entity to be modified.</param>
        /// <returns>Updated Entity.</returns>
        Task<TEntity> Update(TEntity entity);

        /// <summary>
        /// Method to return the entity based on its id which uniquely defines it.
        /// </summary>
        /// <param name="id">The id of the entity.</param>
        /// <returns>Returns entity matching the given id else null</returns>
        Task<TEntity> FindById(int id);

        /// <summary>
        /// Method to fetch all the persisted entities.
        /// </summary>
        /// <returns>List of persisted entities.</returns>
        Task<List<TEntity>> FindAll();

        /// <summary>
        /// Method to return a queryable for a given entity.
        /// </summary>
        /// <returns>Queryable of the entity.</returns>
        IQueryable<TEntity> FindAllQueryable();

        /// <summary>
        /// Method which returns the first entity matching the following predicate
        /// </summary>
        /// <param name="predicate">Lambda expression to be evaluated on the entity</param>
        /// <returns>The first entity matching the predicate.</returns>
        Task<TEntity> FindFirst(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// Retrieves the sub-entity of type <typeparamref name="TSubEntity"/> with the specified Id from the repository.
        /// </summary>
        /// <typeparam name="TSubEntity">The type of sub-entity to be retrieved.</typeparam>
        /// <param name="Id">The Id of the sub-entity to be retrieved.</param>
        /// <returns>The sub-entity of type <typeparamref name="TSubEntity"/> with the specified Id.</returns>
        Task<TSubEntity> GetSubEntityById<TSubEntity>(int Id) where TSubEntity : class, IsEntity;



    }
}

