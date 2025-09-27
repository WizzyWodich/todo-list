namespace TODO.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public virtual List<Todo> Todos { get; set; } = new();
    }
}
