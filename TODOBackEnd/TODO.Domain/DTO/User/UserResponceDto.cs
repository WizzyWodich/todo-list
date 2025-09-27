using TODO.Domain.Models;

namespace TODO.Domain.DTO.User
{
    public record UserResponceDto(
        Guid Id, string UserName, string PasswordHash, string Email, List<Models.Todo> Todos);
}
