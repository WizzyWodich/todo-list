using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TODO.Domain.DTO;
using TODO.Domain.Models;

namespace TODO.Application.Contracts.Service
{
    public interface ITodoService
    {
        Task<IReadOnlyList<TodoResponseDto>> GetAllTodos();
        Task<IReadOnlyList<TodoResponseDto>> GetActiveTodosAsync();
        Task<Todo?> GetTodoByIdAsync(Guid id);
        Task<Todo?> GetTodoByTitleAsync(string title);
        Task<TodoResponseDto> InsertTodoAsync(InsertTodoDto dto);
        Task<bool> UpdateTodoAsync(Guid id, UpdateTodoDto dto);
        Task<bool> UpdateTodoStatusAsync(Guid id);
        Task<bool> DeleteTodoByIdAsync(Guid id);
        Task<int> DeleteManyByIdAsync(List<Guid> guids);
        Task<int> DeleteAllAsync();
    }
}
