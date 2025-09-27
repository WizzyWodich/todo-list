using Microsoft.EntityFrameworkCore;
using TODO.Domain.Contracts.Repository;
using TODO.Domain.Models;
using TODO.Infrastructure.Data;

namespace TODO.Infrastructure.Repository
{
    public class EfTodoRepository(AppDbContext appDbContext) : ITodoRepository
    {
        public async Task<bool> InsertAsync(Todo entity)
        {
            await appDbContext.Todos.AddAsync(entity);
            var result = await appDbContext.SaveChangesAsync();
            return (result > 0);
        }

        public async Task<bool> DeleteAllAsync()
        {
            var result = await appDbContext.Todos.ExecuteDeleteAsync();
            return (result > 0);
        }

        public async Task<bool> DeleteOneAsync(Todo entity)
        {
            appDbContext.Todos.Remove(entity);
            var result = await appDbContext.SaveChangesAsync();
            return (result > 0);
        }

        public async Task<bool> DeleteMoreByIdAsync(List<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Todo> GetAllQueryable()
        {
            return appDbContext.Todos.AsQueryable();
        }

        public async Task<Todo?> GetByIdAsync(Guid id)
        {
            return appDbContext.Todos.FirstOrDefault(e => e.Id == id);
        }

        public async Task<Todo?> GetByTitleAsync(string title)
        {
            return await appDbContext.Todos.Where(t => t.Title
                .Contains(title))
                .FirstOrDefaultAsync();
        }

        
        public async Task<bool> UpdateAsync(Todo entity)
        {
            var task = await appDbContext.Todos.FirstOrDefaultAsync(t => t.Id == entity.Id);
            appDbContext.Todos.Update(task);
            var result = await appDbContext.SaveChangesAsync();
            return (result > 0);
        }

        public async Task<bool> UpdateStatusAsync(Todo entity)
        {
            appDbContext.Todos.Update(entity);
            var result = await appDbContext.SaveChangesAsync();
            return (result > 0);
        }
    }
}
