namespace TODO.Domain.Models
{
    public class Todo
    {

        // TODO: Сменить Guid на UUID
        public Guid Id { get; set; } = Guid.NewGuid(); // PK
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow.Date;
        public bool IsCompleted { get; set; } = false;

        public Guid UserId { get; set; } // FK
        public virtual User User { get; set; }        
    }
}
