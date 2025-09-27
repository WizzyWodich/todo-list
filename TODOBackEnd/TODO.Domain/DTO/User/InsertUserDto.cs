namespace TODO.Domain.DTO.User
{
    public record InsertUserDto(
        string UserName, string Password, string? Email);
}

