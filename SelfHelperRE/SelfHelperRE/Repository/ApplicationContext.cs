using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Diary> Diaries { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Target> Targets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SelfhelperREdb;Trusted_Connection=True;");
        }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
    }
}
