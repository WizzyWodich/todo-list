namespace TODO.Domain.Models
{
    public class Todo
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow.Date;
        public bool IsCompleted { get; set; } = false;
    }
}
