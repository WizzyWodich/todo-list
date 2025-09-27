using TODO.Domain.Models;

namespace TODO.Domain.Contracts.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByUserNameAsync(string userName);
        Task<User?> GetByEmailAsync(string email);
        Task<bool> ExistsByUserNameAsync(string userName);
        Task<bool> ExistsByEmailAsync(string email);
    }
}
