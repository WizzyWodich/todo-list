using Microsoft.EntityFrameworkCore;
using System.Numerics;
using TODO.Domain.Models;

namespace TODO.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>()
                 .HasKey(t => t.Id);

            modelBuilder.Entity<Todo>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Todo>()
                .Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Todo>()
                .Property(t => t.Created)
                .HasDefaultValueSql("NOW()");


            base.OnModelCreating(modelBuilder);
        }
    }
}
