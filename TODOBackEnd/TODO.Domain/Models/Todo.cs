namespace TODO.Domain.Models
{
    public class Todo
    {

        public Guid Id { get; set; } 
        public required string Title { get; set; }
        public string? Description { get; set; }
        public DateTime Created { get; set; }
        public bool IsCompleted { get; set; } = false;

        public Guid UserId { get; set; } // FK
        public User User { get; set; }        
    }
}
