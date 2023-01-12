using BirthdayReminder.Models;
using Microsoft.EntityFrameworkCore;

namespace BirthdayReminder.Database
{
    public interface IDatabaseContext
    {
        DbSet<Person> People { get; set; }
        void AddPersonFromContext(Person person);
        public int SaveChanges();
    }
}