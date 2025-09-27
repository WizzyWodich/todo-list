using Microsoft.EntityFrameworkCore;
using TODO.Domain.Contracts.Repository;
using TODO.Domain.Models;
using TODO.Infrastructure.Data;

namespace TODO.Infrastructure.Repository
{
    public class EfUserRepository(AppDbContext appDbContext) : IUserRepository
    {
        public async Task<bool> DeleteAllAsync()
        {
            var result = await appDbContext.Users.ExecuteDeleteAsync();
            return (result > 0);
        }

        public async Task<bool> DeleteOneAsync(User entity)
        {
            appDbContext.Users.Remove(entity);
            var result = await appDbContext.SaveChangesAsync();
            return (result > 0);
        }

        public async Task<bool> DeleteMoreByIdAsync(List<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistsByUserNameAsync(string userName)
        {
            return await appDbContext.Users.AnyAsync(
                x => x.UserName == userName);
        }

        public IQueryable<User> GetAllQueryable()
        {
            return appDbContext.Users.AsQueryable();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetByUserNameAsync(string userName)
        {
            return await appDbContext.Users
                .FirstOrDefaultAsync(u =>  u.UserName == userName);
        }

        public async Task<bool> InsertAsync(User entity)
        {
            await appDbContext.Users.AddAsync(entity);
            var result = await appDbContext.SaveChangesAsync();
            return (result > 0);
        }

        public async Task<bool> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
