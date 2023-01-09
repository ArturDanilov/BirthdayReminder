using BirthdayReminder.Database;
using BirthdayReminder.Models;

namespace BirthdayReminder.Services
{
    public class BirthdayService
    {
        private BirthdayContext context = new BirthdayContext();

        public List<Person> AllPeople()
        {
            return context.People.ToList();
        }

        public List<Person> FindByDate(DateTime date)
        {
            var people = context.People.Where(x => x.BirthdayDate == date).ToList();
            return people;
        }
    }
}
