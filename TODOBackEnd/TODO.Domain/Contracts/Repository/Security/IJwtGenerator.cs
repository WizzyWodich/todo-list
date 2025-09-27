namespace TODO.Domain.Contracts.Repository.Security
{
    public interface IJwtGenerator<T>
    {
        string GenerateJwtToken(T entity);
    }
}
