using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TODO.Application.Contracts.Service;
using TODO.Domain.Contracts.Repository;
using TODO.Domain.DTO.User;
using TODO.Domain.Models;

namespace TODO.Application.Service
{
    public class UserService(IUserRepository userRepository, IPasswordHasher<User> hasher) : IUserService
    {
        public async Task<IReadOnlyList<UserResponceDto>> GetAllUser()
        {
            var usersList = await userRepository.GetAllQueryable()
                .Select(u => new UserResponceDto(u.Id, u.UserName, u.PasswordHash, u.Email, u.Todos.ToList()))
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
            var user = User.Create(dto.UserName, dto.Password, dto.Email, hasher);

            await userRepository.InsertAsync(user);

            return new UserResponceDto(user.Id, user.UserName, user.PasswordHash, user.Email, user.Todos);
        }
    }
}
