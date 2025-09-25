using Microsoft.EntityFrameworkCore;
using TODO.Application.Contracts.Service;
using TODO.Domain.Contracts.Repository;
using TODO.Domain.DTO;
using TODO.Domain.Models;

namespace TODO.Application.Service
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _repository;

        public TodoService(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<TodoResponseDto>> GetAllTodos()
        {
            var todos = await _repository.GetAllQueryable()
                .Select(t => new TodoResponseDto(t.Id, t.Title, t.Description, t.Created, t.IsCompleted))
                .ToListAsync();

            return todos.AsReadOnly();
        }

        public async Task<IReadOnlyList<TodoResponseDto>> GetActiveTodosAsync()
        {
            var todos = await _repository.GetAllQueryable()
                .Where(t => !t.IsCompleted)
                .OrderBy(t => t.Created)
                .Select(t => new TodoResponseDto(t.Id, t.Title, t.Description, t.Created, t.IsCompleted))
                .ToListAsync();

            return todos.AsReadOnly();
        }

        public async Task<Todo?> GetTodoByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Todo?> GetTodoByTitleAsync(string title)
        {
            if (string.IsNullOrWhiteSpace(title)) return null;
            return await _repository.GetByTitleAsync(title);
        }

        public async Task<TodoResponseDto> InsertTodoAsync(InsertTodoDto dto)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));

            var todo = new Todo
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                Created = DateTime.UtcNow,
                IsCompleted = false
            };

            await _repository.InsertAsync(todo);

            return new TodoResponseDto(todo.Id, todo.Title, todo.Description, todo.Created, todo.IsCompleted);
        }

        public async Task<bool> UpdateTodoAsync(Guid id, UpdateTodoDto dto)
        {
            var todo = await _repository.GetByIdAsync(id);
            if (todo == null) return false;

            if (!string.IsNullOrWhiteSpace(dto.Title))
                todo.Title = dto.Title;
            if (!string.IsNullOrWhiteSpace(dto.Description))
                todo.Description = dto.Description;

            await _repository.UpdateAsync(todo);
            return true;
        }

        public async Task<bool> UpdateTodoStatusAsync(Guid id)
        {
            var todo = await _repository.GetByIdAsync(id);
            if (todo == null) return false;

            todo.IsCompleted = !todo.IsCompleted;
            await _repository.UpdateStatusAsync(todo);
            return true;
        }

        public async Task<bool> DeleteTodoByIdAsync(Guid id)
        {
            var todo = await _repository.GetByIdAsync(id);
            if (todo == null) return false;

            await _repository.DeleteByIdAsync(id);
            return true;
        }

        public async Task<int> DeleteManyByIdAsync(List<Guid> guids)
        {
            var affectedRows = await _repository.GetAllQueryable()
                .Where(t => guids.Contains(t.Id))
                .ExecuteDeleteAsync();

            return affectedRows;
        }

        public async Task<int> DeleteAllAsync()
        {
            var affectedRows = await _repository.GetAllQueryable().ExecuteDeleteAsync();
            return affectedRows;
        }
    }
}
