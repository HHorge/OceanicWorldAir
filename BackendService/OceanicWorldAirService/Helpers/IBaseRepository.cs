namespace OceanicWorldAirService.Helpers
{
    /// <summary>
    /// Base Repository that provides CRUD operations.
    /// </summary>
    /// <typeparam name="T">Entity in Db.</typeparam>
    public interface IBaseRepository<T>
        where T : class
    {
        /// <summary>
        /// Save a new entity to the database.
        /// </summary>
        /// <param name="entity">New Entity to be added.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<T> Create(T entity);

        /// <summary>
        /// Save multiple entities to the database.
        /// </summary>
        /// <param name="entities">List of entities to be added.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<IEnumerable<T>> Create(IEnumerable<T> entities);

        /// <summary>
        /// Save multiple entities to the database.
        /// </summary>
        /// <param name="entities">Arguments of entities to be added.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<IEnumerable<T>> Create(params T[] entities);

        /// <summary>
        /// Read entities from the database.
        /// </summary>
        /// <returns>IQueryable.</returns>
        IQueryable<T> Read();

        /// <summary>
        /// Read entities from the database as no tracking.
        /// </summary>
        /// <returns>IQueryable.</returns>
        IQueryable<T> ReadOnly();

        /// <summary>
        /// Update an entity in the database.
        /// </summary>
        /// <param name="entity">Updated entity.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<T> Update(T entity);

        /// <summary>
        /// Update multiple entities in the database.
        /// </summary>
        /// <param name="entities">List of updated entities.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<IEnumerable<T>> Update(IEnumerable<T> entities);

        /// <summary>
        /// Update multiple entities in the database.
        /// </summary>
        /// <param name="entities">Arguments of entities.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<IEnumerable<T>> Update(params T[] entities);

        /// <summary>
        /// Delete an entity from the database.
        /// </summary>
        /// <param name="entity">Entity that need to be deleted.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<T> Delete(T entity);

        /// <summary>
        /// Delete multiple entities from the database.
        /// </summary>
        /// <param name="entities">List of entities that need to be deleted.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<IEnumerable<T>> Delete(IEnumerable<T> entities);

        /// <summary>
        /// Delete multiple entities from the database.
        /// </summary>
        /// <param name="entities">Arguments of eneities that need to be deleted.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<IEnumerable<T>> Delete(params T[] entities);

        /// <summary>
        /// Get all entities with limit.
        /// DEPRECATED this is not a safe method and will be REMOVED from BaseRespository definition.
        /// </summary>
        /// <param name="limit">limit of number of entities to be retrieved.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ICollection<T>> GetAll(int limit);

        /// <summary>
        /// SaveChanges and Clear ChangeTracker.
        /// DEPRECATED this will be REMOVED soon.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task SaveChangesAndClear();

        /// <summary>
        /// Change EntityState to Added.
        /// </summary>
        /// <param name="entity">Entity which's state to be changed.</param>
        void ChangeStateToAdded(T entity);
    }
}
