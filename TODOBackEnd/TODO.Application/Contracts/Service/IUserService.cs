using TODO.Domain.DTO.User;

namespace TODO.Application.Contracts.Service
{
    public interface IUserService
    {
        Task<IReadOnlyList<UserResponceDto>> GetAllUser();
        Task<UserResponceDto> RegistrationUserAsync(InsertUserDto dto);
        // Task<string?> LoginUserAsync(InsertUserDto dto);
        // Task<bool> UpdateUserAsync();

    }
}
