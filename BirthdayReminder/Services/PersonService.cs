using BirthdayReminder.Database;
using BirthdayReminder.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BirthdayReminder.Services
{
    public class PersonService : IPersonService
    {
        private readonly IDatabaseContext _context;
        public PersonService(IDatabaseContext context)
        {
            this._context = context;
        }

        public List<Person> AllPeople() => _context.People
            .ToList();

        public List<Person> TodayBirthday(DateTime date) => _context.People
            .Where(x => x.BirthdayDate == date)
            .ToList();

        public List<Person> Find29FebruarBirthday(DateTime date) => _context.People
            .Where(x => x.BirthdayDate.Month == 02 && x.BirthdayDate.Day == 29)
            .ToList();
        
        public void AddPersonFromPersonService(Person person) => _context
            .AddPersonFromContext(person);

        public object CreatePerson(string? firstName, string? lastName, DateTime birthday, string? email)
        {
            Person output = new Person();
            output.FirstName = firstName;
            output.LastName = lastName;
            output.BirthdayDate = birthday;
            output.Email = email;

            return output;
        }

        public void DisplayPeople(IHost host)
        {
            var result = GetAllPersons(host.Services);
            foreach (var item in result)
                Console.WriteLine(item.FullName.ToString() + " " + item.BirthdayDate.ToString() + " " + item.Email.ToString() + "\n");
        }

        public void DisplayPeopleTodayBirthday(IHost host)
        {
            var result = GetAllPersons(host.Services);
            Person _person = new Person();

            foreach (var item in result)
            {
                if (item.BirthdayDate.Month == DateTime.Now.Month && item.BirthdayDate.Day == DateTime.Now.Day)
                {
                    _person = item;
                    Console.WriteLine(item.FirstName + " " + item.LastName);
                }
            }
        }
        public void CreateNewPerson(IDatabaseContext context)
        {
            Person person = new Person();
            Console.WriteLine("Firstname - ");
            var firstName = Console.ReadLine();
            Console.WriteLine("Lastname - ");
            var lastName = Console.ReadLine();
            Console.WriteLine("Birthdate (YYYY-MM-DD) - ");
            DateTime birthdate = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("email - ");
            var email = Console.ReadLine();

            context.AddPersonFromContext(new Person(firstName, lastName, birthdate, email));
            context.SaveChanges();
        }

        public List<Person> GetAllPersons(IServiceProvider services)
        {
            IPersonService personService = services.GetRequiredService<IPersonService>(); // new PersonService(new DatabaseContext());
            return personService.AllPeople();
        }

        public void ReloadDb(IDatabaseContext context)
        {
            context.AddPersonFromContext(new Person("Geber", "Ann", new DateTime(1970, 11, 01), "ali@adesso.com"));
            context.AddPersonFromContext(new Person("Zug", "Ann", new DateTime(1980, 01, 01), "sven@adesso.com"));
            context.AddPersonFromContext(new Person("Lise", "Anna", new DateTime(1990, 01, 01), "markus@adesso.com"));
            context.AddPersonFromContext(new Person("Nass", "Anna", new DateTime(1960, 01, 01), "artur@adesso.com"));
            context.AddPersonFromContext(new Person("Conda", "Anna", new DateTime(2023, 01, 01), "anna@adesso.com"));
            context.AddPersonFromContext(new Person("Müll", "Reiner", new DateTime(2023, 01, 02), "benz@adesso.com"));
            context.AddPersonFromContext(new Person("Bier", "Wilma", new DateTime(2023, 01, 03), "wilmaBier@adesso.com"));
            context.AddPersonFromContext(new Person("Huana", "Mary", new DateTime(2023, 01, 04), "bot4@adesso.com"));

            context.SaveChanges();
        }
    }
}
