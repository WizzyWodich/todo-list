namespace TODO.Domain.Contracts.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        IQueryable<T> GetAllQueryable();

        Task<bool> InsertAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteOneAsync(T entity);
        Task<bool> DeleteMoreByIdAsync(List<Guid> ids);
        Task<bool> DeleteAllAsync();
    }
}
