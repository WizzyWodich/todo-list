using Microsoft.AspNetCore.Mvc;
using TODO.Application.Contracts.Service;
using TODO.Domain.DTO.Todo;

namespace TODO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController(ITodoService todoService) : ControllerBase
    {
             
        // GET: api/todo
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<TodoResponseDto>>> GetAllTodos()
        {
            var todos = await todoService.GetAllTodos();
            return Ok(todos);
        }

        // GET: api/todo/active
        [HttpGet("active")]
        public async Task<ActionResult<IReadOnlyList<TodoResponseDto>>> GetActiveTodos()
        {
            var todos = await todoService.GetActiveTodosAsync();
            return Ok(todos);
        }

        // GET: api/todo/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoResponseDto>> GetTodoById(Guid id)
        {
            var todo = await todoService.GetTodoByIdAsync(id);
            if (todo == null) return NotFound();
            return Ok(todo);
        }

        // POST: api/todo
        [HttpPost]
        public async Task<ActionResult<TodoResponseDto>> CreateTodo([FromBody] InsertTodoDto dto)
        {
            if (dto is null) return BadRequest();

            var created = await todoService.InsertTodoAsync(dto);

            return CreatedAtAction(
                nameof(GetTodoById),
                new { id = created.Id },
                created
            );
        }

        // PUT: api/todo/{id} - обновление всего Todo
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(Guid id, [FromBody] UpdateTodoDto dto)
        {
            var updated = await todoService.UpdateTodoAsync(id, dto);
            if (!updated) return NotFound();
            return NoContent();
        }

        // PATCH: api/todo/status/{id} - переключение статуса
        [HttpPatch("status/{id}")]
        public async Task<IActionResult> ToggleTodoStatus(Guid id)
        {
            var updated = await todoService.UpdateTodoStatusAsync(id);
            if (!updated) return NotFound();
            return NoContent();
        }

        // DELETE: api/todo/{id} - удалить один
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(Guid id)
        {
            var deleted = await todoService.DeleteTodoByIdAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        //// DELETE: api/todo/batch - удалить несколько
        //[HttpDelete("batch")]
        //public async Task<IActionResult> DeleteTodos([FromBody] Guid[] ids)
        //{
        //    if (ids == null || ids.Length == 0) return BadRequest();

        //    var affectedRows = await todoService.DeleteTodosByIdsAsync(ids);
        //    return Ok(new { deleted = affectedRows });
        //}

        // DELETE: api/todo - удалить все
        [HttpDelete]
        public async Task<IActionResult> DeleteAllTodos()
        {
            var affectedRows = await todoService.DeleteAllAsync();
            return Ok(new { deleted = affectedRows });
        }
    }
}
