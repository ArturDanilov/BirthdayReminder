using BirthdayReminder.Database;
using BirthdayReminder.Models;
using BirthdayReminder.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BirthdayReminder.Telegram;

namespace BirthdayReminder.UI
{
    public class ConsoleUI
    {
        public void ConsoleStart(IHost host, IDatabaseContext databaseContext)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(
                "1 - Personen anzeigen" +
                "\n2 - Personen hinzufügen" +
                "\n3 - DB neu ausfühlen" +
                "\n4 - Telegram Bot" +
                "\n5 - Wer ist heute geboren?" +
                "\n6 - Wer ist morgen geboren?" +
                "\n7 - Wollen wir jemand gratulieren?" +
                "\n0 - App schließen" +
                "\n\nDeine Auswahl ist...");

            var selectedAction = "";
            do
            {
                selectedAction = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;

                switch (selectedAction)
                {
                    case "1":
                        DisplayAllPeople(host);
                        break;
                    case "2":
                        CreateNewPerson(databaseContext);
                        break;
                    case "3":
                        ReloadDb(databaseContext);
                        break;
                    case "4":
                        var telegramService = new TelegramService();
                        telegramService.CallTelegramBot();
                        break;
                    case "5":
                        CreatePersonBirthdayToday(databaseContext); //TODO: tempo.daten
                        DisplayPeopleTodayBirthday(host);
                        break;
                    case "6":
                        CreatePersonBirthdayTomorow(databaseContext); //TODO: tempo.daten
                        DisplayPeopleTomorowBirthday(host);
                        break;
                    case "7":
                        Console.WriteLine("\nWollen wir mit Hilfe chat.openai um jemandem zu gratulieren?");
                        var antwort = Console.ReadLine();
                        switch (antwort)
                        {
                            case "y":
                                System.Threading.Thread.Sleep(1000);
                                Console.WriteLine("Du kannst chat.openai nutzen");
                                ChatOpenai();
                                break;
                            case "n":
                                Console.WriteLine("Service kapput");
                                break;
                            default:
                                break;
                        }
                        break;
                    case "0":
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Program Ende");
                        break;
                    default:
                        Console.WriteLine("Drücken Sie die Eingabetaste und versuchen Sie es erneut oder drücken Sie 0 zum Schließen!");
                        break;
                }

            } while (selectedAction != "0");
        }
        
        List<Person> GetAllPersons(IServiceProvider services)//????
        {
            IPersonService personService = services.GetRequiredService<IPersonService>();// new PersonService(new DatabaseContext());
            return personService.AllPeople();
        }

        private void DisplayAllPeople(IHost host)
        {
            var result = GetAllPersons(host.Services);
            foreach (var item in result)
                Console.WriteLine(item.FullName.ToString() + " | " + item.BirthdayDate.Date.ToString("dd/MM/yyyy") + " | " + item.Email.ToString() + "\n");
        }

        private void DisplayPeopleTodayBirthday(IHost host)
        {
            var result = GetAllPersons(host.Services);
            //var _person = new Person();

            foreach (var item in result)
            {
                if (item.BirthdayDate.Month == DateTime.Now.Month 
                    && item.BirthdayDate.Day == DateTime.Now.Day)
                {
                    //_person = item;
                    Console.WriteLine(item.FirstName + " " + item.LastName);
                }
            }
        }
        private void DisplayPeopleTomorowBirthday(IHost host)
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
                    Console.WriteLine(item.FirstName + " " + item.LastName + " soll heute gratuliert werden");
                }
                if (item.BirthdayDate.Month == DateTime.Now.Month && item.BirthdayDate.Day == DateTime.Now.Day + 1)
                {
                    //_person = item;
                    Console.WriteLine(item.FirstName + " " + item.LastName + " soll  gratuliert werden");
                }
            }
        }

        void CreateNewPerson(IDatabaseContext context)
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

        void CreatePersonBirthdayToday(IDatabaseContext context)
        {
            context.AddPersonFromContext(new Person("Todaev", "Birthdman", DateTime.Now.Date, "testEmail"));
            context.SaveChanges();
        }

        void CreatePersonBirthdayTomorow(IDatabaseContext context)
        {
            context.AddPersonFromContext(new Person("Tomorowich", "Birthdman", DateTime.Now.Date.AddDays(+1), "testEmail"));
            context.SaveChanges();
        }


        void ReloadDb(IDatabaseContext context)
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
        private void ChatOpenai()
        {
            Console.WriteLine("chatopenai started...");
        }
    }
}
