using BirthdayReminder.Database;
using BirthdayReminder.Models;
using BirthdayReminder.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System.Globalization;
using System.Runtime.CompilerServices;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace BirthdayReminder
{
    public class Program
    {
        private static string token { get; set; } = "5709592372:AAEo8ZYJgISbeXSXejscODAu0OIuu2ZN7UE";
        private static TelegramBotClient client;
        private PersonService personService;


        static void Main(string[] args)
        {
            using IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddDbContext<DatabaseContext>() //return new DatabasContext();
                    .AddSingleton<IPersonService, PersonService>() //singelton работает столько сколько работает сервис
                    .AddSingleton<IDatabaseContext>(provider => provider.GetService<DatabaseContext>()); //
                })
                .Build();

            IDatabaseContext databaseContext = host.Services.GetRequiredService<IDatabaseContext>();
            

            Console.WriteLine(
                "1 - Personen zeigen" +
                "\n2 - Personen hinzufügen" +
                "\n3 - DB actuialisieren" +
                "\n4 - Telegram Bot" +
                "\n5 - Wer ist heute geboren?" +
                "\n0 - App schließen" +
                "\n\nDeine Auswahl ist...");

            string selectedAction = "";
            do
            {
                selectedAction = Console.ReadLine();

                switch (selectedAction)
                {
                    case "1":
                        DisplayPeople(host);
                        break;
                    case "2":
                        CreateNewPerson(databaseContext);
                        break;
                    case "3":
                        ReloadDb(databaseContext);     //add and save created persons to DB
                        break;
                    case "4":
                        CallTelegram();
                        break;
                    case "5":
                        DisplayPeopleTodayBirthday(host);
                        break;
                    case "0":
                        Console.WriteLine("Program Ende");
                        break;
                    default:
                        Console.WriteLine("Hit enter und try again or hit 0 to close!");
                        break;
                }

            } while (selectedAction != "0");
        }

        private static void CallTelegram()
        {
            Program program = new Program();
            client = new TelegramBotClient(token);
            client.StartReceiving();
            client.OnMessage += program.OnMessageHandler;
            Console.ReadLine();
            client.StopReceiving();
        }

        private static void DisplayPeople(IHost host)
        {
            var result = GetAllPersons(host.Services);
            foreach (var item in result)
            {
                Console.WriteLine(item.FullName.ToString() + " " + item.BirthdayDate.ToString() + " " + item.Email.ToString() + "\n");
            }
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
        private async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            var msg = e.Message;
            if (msg.Text != null)
            {
                Console.WriteLine($"Message mit dem Text: {msg.Text}");
                //await client.SendTextMessageAsync(msg.Chat.Id, msg.Text, replyMarkup: GetButtons());
                switch (msg.Text)
                {
                    case "Stic":
                        var stic = await client.SendStickerAsync(
                            chatId: msg.Chat.Id,
                            sticker: "https://cdn.tlgrm.app/stickers/ccd/a8d/ccda8d5d-d492-4393-8bb7-e33f77c24907/192/1.webp",
                            replyToMessageId: msg.MessageId,
                            replyMarkup: GetButtons());
                        break;
                    case "Happy":
                        var stic2 = await client.SendStickerAsync(
                            chatId: msg.Chat.Id,
                            sticker: "https://tlgrm.ru/_/stickers/ccd/a8d/ccda8d5d-d492-4393-8bb7-e33f77c24907/9.webp",
                            replyToMessageId: msg.MessageId,
                            replyMarkup: GetButtons());
                        break;
                    case "Cry":
                        var stic3 = await client.SendStickerAsync(
                            chatId: msg.Chat.Id,
                            sticker: "https://tlgrm.ru/_/stickers/ccd/a8d/ccda8d5d-d492-4393-8bb7-e33f77c24907/4.webp",
                            replyToMessageId: msg.MessageId,
                            replyMarkup: GetButtons());
                        break;
                    case "Wer ist heute geboren?":
                        var heuteGeboren = await client.SendTextMessageAsync(
                            chatId: msg.Chat.Id,
                            text: $"Heute war {DisplayPeopleTodayBirthday} geboren",
                            replyMarkup: GetButtons());
                        break;
                    //case "Wer ist morgen geboren?":
                    //    var morgenGeboren = await client.SendTextMessageAsync(
                    //        chatId: msg.Chat.Id,
                    //        text: $"Morgen war {birthdayReminder.GetAllPersonsNamesTomorowBirthday()}geboren",
                    //        replyMarkup: GetButtons());
                    //    break;
                    //case "Ein Grüß sagen":
                    //    var gruseSagen = await client.SendTextMessageAsync(
                    //        chatId: msg.Chat.Id,
                    //        text: $"Ein Grüß war {birthdayReminder.GetAllPersonsNamesTodayBirthday()}geschickt",
                    //        replyMarkup: GetButtons());
                    //    break;
                    default:
                        await client.SendTextMessageAsync(msg.Chat.Id, "Chouse comand: ", replyMarkup: GetButtons());
                        break;
                }
            }
        }

        private static IReplyMarkup GetButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{new KeyboardButton { Text = "Wer ist heute geboren?" },new KeyboardButton { Text = "Wer ist morgen geboren?"} },
                    new List<KeyboardButton>{new KeyboardButton { Text = "Cry"},new KeyboardButton { Text = "Happy"} },
                    new List<KeyboardButton>{new KeyboardButton { Text = "Ein Grüß sagen"} }
                }
            };
        }
    }
}