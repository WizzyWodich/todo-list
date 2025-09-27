using Microsoft.EntityFrameworkCore;
using TODO.Application.Contracts.Service;
using TODO.Domain.Contracts.Repository;
using TODO.Domain.DTO.Todo;
using TODO.Domain.Models;

namespace TODO.Application.Service
{
    public class TodoService(ITodoRepository repository) : ITodoService
    {
        public async Task<IReadOnlyList<TodoResponseDto>> GetAllTodos()
        {
            var todos = await repository.GetAllQueryable()
                .Select(t => new TodoResponseDto(t.Id, t.Title, t.Description, t.Created, t.IsCompleted))
                .ToListAsync();

            return todos.AsReadOnly();
        }

        public async Task<IReadOnlyList<TodoResponseDto>> GetActiveTodosAsync()
        {
            var todos = await repository.GetAllQueryable()
                .Where(t => !t.IsCompleted)
                .OrderBy(t => t.Created)
                .Select(t => new TodoResponseDto(t.Id, t.Title, t.Description, t.Created, t.IsCompleted))
                .ToListAsync();

            return todos.AsReadOnly();
        }

        public async Task<TodoResponseDto?> GetTodoByIdAsync(Guid id)
        {
            var response =  await repository.GetByIdAsync(id);

            return new TodoResponseDto(
                response.Id, response.Title, response.Description, response.Created, response.IsCompleted);
        }

        public async Task<TodoResponseDto?> GetTodoByTitleAsync(string title)
        {
            if (string.IsNullOrWhiteSpace(title)) return null;
            var response = await repository.GetByTitleAsync(title);
            return new TodoResponseDto(
                response.Id, response.Title, response.Description, response.Created, response.IsCompleted);
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

            await repository.InsertAsync(todo);

            return new TodoResponseDto(todo.Id, todo.Title, todo.Description, todo.Created, todo.IsCompleted);
        }

        public async Task<bool> UpdateTodoAsync(Guid id, UpdateTodoDto dto)
        {
            var todo = await repository.GetByIdAsync(id);
            if (todo == null) return false;

            if (!string.IsNullOrWhiteSpace(dto.Title))
                todo.Title = dto.Title;
            if (!string.IsNullOrWhiteSpace(dto.Description))
                todo.Description = dto.Description;

            await repository.UpdateAsync(todo);
            return true;
        }

        public async Task<bool> UpdateTodoStatusAsync(Guid id)
        {
            var todo = await repository.GetByIdAsync(id);
            if (todo == null) return false;

            todo.IsCompleted = !todo.IsCompleted;
            await repository.UpdateStatusAsync(todo);
            return true;
        }

        public async Task<bool> DeleteTodoByIdAsync(Guid id)
        {
            var todo = await repository.GetByIdAsync(id);
            if (todo == null) return false;

            await repository.DeleteOneAsync(todo);
            return true;
        }

        public async Task<int> DeleteManyByIdAsync(List<Guid> guids)
        {
            var affectedRows = await repository.GetAllQueryable()
                .Where(t => guids.Contains(t.Id))
                .ExecuteDeleteAsync();

            return affectedRows;
        }

        public async Task<int> DeleteAllAsync()
        {
            var affectedRows = await repository.GetAllQueryable().ExecuteDeleteAsync();
            return affectedRows;
        }
    }
}
