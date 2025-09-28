using Microsoft.AspNetCore.Identity;

namespace TODO.Domain.Models
{
    public class User
    {
        public Guid Id { get; private set; }
        public string UserName { get; private set; }
        public string PasswordHash { get; private set; }
        public string Email { get; private set; }
        public List<Todo> Todos { get; private set; } = new();

        protected User() { } 

        private User(string userName, string passwordHash, string email)
        {
            UserName = userName;
            PasswordHash = passwordHash;
            Email = email;
        }

        public static User Create(string userName, string password, string email, IPasswordHasher<User> hasher)
        {
            var tempUser = new User(userName, string.Empty, email);
            var hash = hasher.HashPassword(tempUser, password);
            tempUser.PasswordHash = hash;
            return tempUser;
        }

        public bool VerifyPassword(string password, IPasswordHasher<User> hasher)
        {
            var result = hasher.VerifyHashedPassword(this, PasswordHash, password);
            return result == PasswordVerificationResult.Success;
        }
    }

}
