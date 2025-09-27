using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TODO.Application.Contracts.Service;
using TODO.Domain.Contracts.Repository;
using TODO.Domain.Models;
using TODO.Domain.DTO.User;

namespace TODO.Application.Service
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        public async Task<IReadOnlyList<UserResponceDto>> GetAllUser()
        {
            var usersList = await userRepository.GetAllQueryable()
                .Select(u => new UserResponceDto(u.Id, u.UserName, u.Password, u.Email, u.Todos.ToList()))
                .ToListAsync();

            return usersList.AsReadOnly();
        }

        //public async Task<string?> LoginUserAsync(InsertUserDto dto)
        //{
        //    if (dto is null) throw new ArgumentNullException(nameof(dto));
        //    var user = await userRepository.GetByUserNameAsync(dto.UserName);
        //    var existsByUserNameresult = await userRepository.ExistsByUserNameAsync(user.UserName);
        //    if (existsByUserNameresult) return "";

        //    var verifyPassword = new PasswordHasher<User>()
        //        .VerifyHashedPassword(user, user.Password, dto.Password);



        //    // TODO: JWT токен генерация
        //}

        public async Task<UserResponceDto> RegistrationUserAsync(InsertUserDto dto)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));

            var user = new User
            {
                UserName = dto.UserName,
                Email = dto.Email,
            };
            var passwordHash = new PasswordHasher<User>().HashPassword(user, dto.Password);
            user.Password = passwordHash;            
            await userRepository.InsertAsync(user);

            return new UserResponceDto(user.Id, user.UserName, user.Password, user.Email, user.Todos);
        }
    }
}
