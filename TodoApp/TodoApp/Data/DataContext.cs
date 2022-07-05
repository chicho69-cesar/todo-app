using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoApp.Data.Entities;

namespace TodoApp.Data {
    public class DataContext : IdentityDbContext<User> {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {

        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<TaskWork> TaskWorks { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Group>().HasIndex(g => g.Name).IsUnique();
            modelBuilder.Entity<UserGroup>().HasIndex("UserId", "GroupId").IsUnique();
        }
    }
}