using BirthdayReminder.Database;
using BirthdayReminder.Models;

namespace BirthdayReminder.Services
{
    public class PersonService : IPersonService
    {
        private readonly IDatabaseContext _context;
        public PersonService(IDatabaseContext context)
        {
            this._context = context;
        }

        public List<Person> AllPeople() //CheckAllPeopleHasCalledPeopleFromContextOnce
        {
            return _context.People.ToList();
        }
        public List<Person> FindByDate(DateTime date)
        {
            var people = _context.People.Where(x => x.BirthdayDate == date).ToList();
            return people;
        }
        public List<Person> Find29FebruarBirthday(DateTime date)
        {
            var people = _context.People.Where(x => x.BirthdayDate.Month == 02 && x.BirthdayDate.Day == 29).ToList();
            return people;
        }

        public object CreatePerson(string? firstName, string? lastName, DateTime birthday, string? email)
        {
            Person output = new Person();

            output.FirstName = firstName;
            output.LastName = lastName;
            output.BirthdayDate = birthday;
            output.Email = email;

            return output;
        }

        public void AddPersonFromPersonService(Person person)
        {
            this._context.AddPersonFromContext(person);
        }
    }
}
