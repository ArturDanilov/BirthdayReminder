using BirthdayReminder.Models;
using Microsoft.EntityFrameworkCore;

namespace BirthdayReminder.Database
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DbSet<Person> People { get; set; }
        public string DatabasePath { get; }

        public DatabaseContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DatabasePath = Path.Join(path, "birthday.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite($"Data Source={DatabasePath}");

        public void AddPersonFromContext(Person person)
        {
              People.Add(person);
            
        }
        public int SaveChanges()
        {
             return base.SaveChanges();    
        }
        
    }
}
