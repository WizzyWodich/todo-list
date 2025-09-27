using TODO.Domain.Contracts.Repository;
using TODO.Domain.Models;

namespace TODO.Domain.Contracts.Repository
{
    public interface ITodoRepository : IRepository<Todo>
    {
        Task<Todo?> GetByTitleAsync(string title);
        Task<bool> UpdateStatusAsync(Todo entity);
    }
}
