namespace AirlineApp.Domain.Interfaces;

/// <summary>
/// Generic repository for working with entities.
/// Provides basic CRUD operations: retrieve all, get by Id, check existence, add, update, and delete.
/// </summary>
/// <typeparam name="T">The entity type the repository works with</typeparam>
public interface IGenericRepository<T> where T : class
{
    /// <summary>
    /// Retrieve all entities of type T.
    /// </summary>
    /// <returns>A collection of all entities of type T</returns>
    public Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// Retrieve an entity by its identifier.
    /// </summary>
    /// <param name="id">The entity's identifier</param>
    /// <returns>The entity if found; otherwise, null</returns>
    public Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// Check whether an entity with the specified Id exists.
    /// </summary>
    /// <param name="id">The entity's identifier</param>
    /// <returns>True if the entity exists; otherwise, false</returns>
    public Task<bool> ExistsByIdAsync(int id);

    /// <summary>
    /// Add a new entity to the repository.
    /// </summary>
    /// <param name="entity">The entity to add</param>
    public Task AddAsync(T entity);

    /// <summary>
    /// Update an existing entity in the repository.
    /// </summary>
    /// <param name="entity">The entity with updated values</param>
    public Task UpdateAsync(T entity);

    /// <summary>
    /// Delete an entity by its identifier.
    /// </summary>
    /// <param name="id">The entity's identifier</param>
    public Task DeleteAsync(int id);
}
