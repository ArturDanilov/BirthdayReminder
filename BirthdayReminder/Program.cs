using BirthdayReminder.Database;
using BirthdayReminder.Models;
using BirthdayReminder.Services;
using BirthdayReminder.Telegram;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace BirthdayReminder
{
    public class Program
    {
        static void Main(string[] args)
        {
            using IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddDbContext<DatabaseContext>() //return new DatabasContext();
                    .AddSingleton<IPersonService, PersonService>() //singelton работает столько сколько работает сервис
                    .AddSingleton<IDatabaseContext>(provider => provider.GetService<DatabaseContext>());
                })
                .Build();
            IDatabaseContext databaseContext = host.Services.GetRequiredService<IDatabaseContext>();

            ConsoleStart(host, databaseContext);
        }

        //ConsoleUI
        private static void ConsoleStart(IHost host, IDatabaseContext databaseContext)
        {
            Program program = new Program();
            string selectedAction = "";
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(
                "1 - Personen zeigen" +
                "\n2 - Personen hinzufügen" +
                "\n3 - DB ausfühlen" +
                "\n4 - Telegram Bot" +
                "\n5 - Wer ist heute geboren?" +
                "\n0 - App schließen" +
                "\n\nDeine Auswahl ist...");

            do
            {
                selectedAction = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;

                switch (selectedAction)
                {
                    case "1":
                        program.DisplayPeople(host);
                        break;
                    case "2":
                        CreateNewPerson(databaseContext);
                        break;
                    case "3":
                        ReloadDb(databaseContext);
                        break;
                    case "4":
                        TelegramService telegramService = new TelegramService();
                        telegramService.CallTelegramBot();
                        break;
                    case "5":
                        DisplayPeopleTodayBirthday(host);
                        break;
                    case "0":
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Program Ende");
                        break;
                    default:
                        Console.WriteLine("Hit enter und try again or hit 0 to close!");
                        break;
                }

            } while (selectedAction != "0");
        }

        private void DisplayPeople(IHost host)
        {
            var result = GetAllPersons(host.Services);
            foreach (var item in result)
                Console.WriteLine(item.FullName.ToString() + " " + item.BirthdayDate.ToString() + " " + item.Email.ToString() + "\n");
        }

        private static void DisplayPeopleTodayBirthday(IHost host)
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

        //public static string SendBirthdayToTelegram(IHost host)
        //{
        //    var result = GetAllPersons(host.Services);
        //    string namePerson = "";
        //    Person _person = new Person();

        //    foreach (var item in result)
        //    {
        //        if (item.BirthdayDate.Month == DateTime.Now.Month && item.BirthdayDate.Day == DateTime.Now.Day)
        //        {
        //            namePerson = item.FirstName;
        //            return namePerson;
        //        }
        //    }
        //}

        static void CreateNewPerson(IDatabaseContext context)
        {
            Person person= new Person();

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

        static List<Person> GetAllPersons(IServiceProvider services)
        {
            IPersonService personService = services.GetRequiredService<IPersonService>(); // new PersonService(new DatabaseContext());
            return personService.AllPeople();
        }

        static void ReloadDb(IDatabaseContext context)
        {
                context.AddPersonFromContext(new Person("Geber","Ann",  new DateTime(1970, 11, 01), "ali@adesso.com"));
                context.AddPersonFromContext(new Person("Zug","Ann",  new DateTime(1980, 01, 01), "sven@adesso.com"));
                context.AddPersonFromContext(new Person( "Lise","Anna", new DateTime(1990, 01, 01), "markus@adesso.com"));
                context.AddPersonFromContext(new Person("Nass", "Anna", new DateTime(1960, 01, 01), "artur@adesso.com"));
                context.AddPersonFromContext(new Person("Conda", "Anna", new DateTime(2023, 01, 01), "anna@adesso.com"));
                context.AddPersonFromContext(new Person( "Müll","Reiner", new DateTime(2023, 01, 02), "benz@adesso.com"));
                context.AddPersonFromContext(new Person("Bier", "Wilma", new DateTime(2023, 01, 03), "wilmaBier@adesso.com"));
                context.AddPersonFromContext(new Person("Huana", "Mary", new DateTime(2023, 01, 04), "bot4@adesso.com"));

                context.SaveChanges();            
        }
    }
}