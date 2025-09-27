using Microsoft.AspNetCore.Authentication.JwtBearer;
using TODO.Domain.Contracts.Repository.Security;
using TODO.Domain.Models;
using TODO.API.Settings;

namespace TODO.Infrastructure.Security
{
    public class JwtGenerator(JwtSettings) : IJwtGenerator<User>
    {
        public string GenerateJwtToken(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
