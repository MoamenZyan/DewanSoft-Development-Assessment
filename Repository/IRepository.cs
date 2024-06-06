public interface IRepository<T>
{
    public Task<T?> AddAsync(T entity);
    public Task<List<T>> GetAllAsync();
    public Task<bool> Delete(T entity);
    public Task<T?> GetById(int id);
    public T? GetByPredicate(Func<T, bool> func);
    public Task Update(T entity);
}