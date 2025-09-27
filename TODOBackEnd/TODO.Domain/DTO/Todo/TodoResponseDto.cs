namespace TODO.Domain.DTO.Todo
{
    public record TodoResponseDto(Guid Id, string Title, string Description, DateTime Created, bool IsCompleted);
}
