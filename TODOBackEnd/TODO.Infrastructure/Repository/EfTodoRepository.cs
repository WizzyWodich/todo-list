using Microsoft.EntityFrameworkCore;
using TODO.Domain.Contracts.Repository;
using TODO.Domain.Models;
using TODO.Infrastructure.Data;

namespace TODO.Infrastructure.Repository
{
    public class EfTodoRepository : ITodoRepository
    {
        private readonly AppDbContext _appDbContext;

        public EfTodoRepository (AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task InsertAsync(Todo entity)
        {
            await _appDbContext.Todos.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAllAsync()
        {
            await _appDbContext.Todos.ExecuteDeleteAsync();
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var entity = await _appDbContext.Todos.FirstOrDefaultAsync(e => e.Id == id);
            _appDbContext.Todos.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteMoreByIdAsync(List<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Todo> GetAllQueryable()
        {
            return _appDbContext.Todos.AsQueryable();
        }

        public async Task<Todo?> GetByIdAsync(Guid id)
        {
            return _appDbContext.Todos.FirstOrDefault(e => e.Id == id);
        }

        public async Task<Todo?> GetByTitleAsync(string title)
        {
            return await _appDbContext.Todos.Where(t => t.Title
                .Contains(title))
                .FirstOrDefaultAsync();
        }

        
        public async Task UpdateAsync(Todo entity)
        {
            var task = await _appDbContext.Todos.FirstOrDefaultAsync(t => t.Id == entity.Id);
            _appDbContext.Todos.Update(task);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateStatusAsync(Todo entity)
        {
            _appDbContext.Todos.Update(entity);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
