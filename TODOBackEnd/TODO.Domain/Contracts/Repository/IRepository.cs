namespace TODO.Domain.Contracts.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        IQueryable<T> GetAllQueryable();

        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteByIdAsync(Guid id);
        Task DeleteMoreByIdAsync(List<Guid> ids);
        Task DeleteAllAsync();
    }
}
