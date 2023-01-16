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
            _context = context;
        }

        public List<Person> AllPeople() => _context.People
            .ToList();

        public void DisplayPeopleTodayBirthday(IHost host)
        {
            var result = GetAllPersons(host.Services);
            //var _person = new Person();

            foreach (var item in result)
            {
                if (item.BirthdayDate.Month == DateTime.Now.Month
                    && item.BirthdayDate.Day == DateTime.Now.Day)
                {
                    //_person = item;
                    Console.WriteLine(item.FirstName 
                        + " " + item.LastName 
                        + " wurde an diesem schönen Tag geboren");
                }
            }
        }

        public List<Person> GetAllPersons(IServiceProvider services)//????
        {
            IPersonService personService = services
                .GetRequiredService<IPersonService>();// new PersonService(new DatabaseContext());
            return personService.AllPeople();
        }

        public void DisplayAllPeople(IHost host)
        {
            var result = GetAllPersons(host.Services);
            foreach (var item in result)
                Console.WriteLine(item.FullName
                    .ToString() + " | " + item.BirthdayDate.Date
                    .ToString("dd/MM/yyyy") + " | " + item.Email
                    .ToString() + "\n");
        }



        public void DisplayPeopleTomorowBirthday(IHost host)
        {
            var result = GetAllPersons(host.Services);
            //var _person = new Person();

            foreach (var item in result)
            {
                if (item.BirthdayDate.Month == DateTime.Now.Month
                    && item.BirthdayDate.Day == DateTime.Now.Day
                    && item.BirthdayDate.Month == 2
                    && item.BirthdayDate.Day == 29)
                {
                    //_person = item;
                    Console.WriteLine(" Sie können "
                        + item.FirstName + " " + item.LastName
                        + " nur ein mal pro vier Jahre gratulieren, aber wenn Sie es wirklich wollen, können Sie es heute tun.");
                }
                if (item.BirthdayDate.Month == DateTime.Now.Month && item.BirthdayDate.Day == DateTime.Now.Day + 1)
                {
                    //_person = item;
                    Console.WriteLine(item.FirstName 
                        + " " + item.LastName 
                        + "  hätte an diesem schönen Tag geboren werden können, wurde aber morgen geboren");
                }
            }
        }


        public void CreateNewPerson(IDatabaseContext context)
        {
            Console.WriteLine("Enter firstname...");
            var firstName = Console.ReadLine();

            Console.WriteLine("Enter lastname...");
            var lastName = Console.ReadLine();

            Console.WriteLine("Enter birthdate (YYYY-MM-DD)...");
            DateTime birthdate = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("Enter email...");
            var email = Console.ReadLine();

            context.AddPersonFromContext(new Person(firstName, lastName, birthdate.Date, email));
            context.SaveChanges();
        }

        public void CreatePersonBirthdayToday(IDatabaseContext context)
        {
            context.AddPersonFromContext(new Person("Todaev", "Birthdman", DateTime.Now.Date, "testEmail"));
            context.SaveChanges();
        }

        public void CreatePersonBirthdayTomorow(IDatabaseContext context)
        {
            context.AddPersonFromContext(new Person("Tomorowich", "Birthdman", DateTime.Now.Date.AddDays(+1), "testEmail"));
            context.SaveChanges();
        }

        public void ReloadDb(IDatabaseContext context)
        {
            context.AddPersonFromContext(new Person("Geber", "Ann", new DateTime(1970, 11, 01).Date, "ali@adesso.com"));
            context.AddPersonFromContext(new Person("Zug", "Ann", new DateTime(1980, 01, 01).Date, "sven@adesso.com"));
            context.AddPersonFromContext(new Person("Lise", "Anna", new DateTime(1990, 01, 01).Date, "markus@adesso.com"));
            context.AddPersonFromContext(new Person("Nass", "Anna", new DateTime(1960, 01, 01).Date, "artur@adesso.com"));
            context.AddPersonFromContext(new Person("Conda", "Anna", new DateTime(2023, 01, 01).Date, "anna@adesso.com"));
            context.AddPersonFromContext(new Person("Müll", "Reiner", new DateTime(2023, 01, 02).Date, "benz@adesso.com"));
            context.AddPersonFromContext(new Person("Bier", "Wilma", new DateTime(2023, 01, 03).Date, "wilmaBier@adesso.com"));
            context.AddPersonFromContext(new Person("Huana", "Mary", new DateTime(2023, 01, 04).Date, "bot4@adesso.com"));

            context.SaveChanges();
        }

        //TODO
        public void ChatOpenai()
        {
            Console.WriteLine("chatopenai started...");
        }



        /// <summary>
        /// TODO:...darunter alles nicht sinvoll ist 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>

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
            output.BirthdayDate = birthday.Date;
            output.Email = email;

            return output;
        }
    }
}
