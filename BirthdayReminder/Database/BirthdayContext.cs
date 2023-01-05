using Microsoft.EntityFrameworkCore;

namespace BirthdayReminder.Database
{
    public class BirthdayContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public string DatabasePath { get; }

        public BirthdayContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DatabasePath = Path.Join(path, "birthday.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseSqlite($"Data Source={DatabasePath}");
    }
}
