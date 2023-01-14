using BirthdayReminder.Database;
using BirthdayReminder.Models;
using BirthdayReminder.Services;
using BirthdayReminder.Telegram;
using BirthdayReminder.UI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BirthdayReminder
{
    public class Program
    {
        static void Main(string[] args)
        {
            //?
            using IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddDbContext<DatabaseContext>() //return new DatabasContext();
                    .AddSingleton<IPersonService, PersonService>() //singelton работает столько сколько работает сервис
                    .AddSingleton<ITelegramService, TelegramService>()
                    .AddSingleton<IDatabaseContext>(provider => provider.GetService<DatabaseContext>());
                })
                .Build();

            IDatabaseContext databaseContext = host.Services.GetRequiredService<IDatabaseContext>(); //?

            ConsoleUI consoleUI = new ConsoleUI();
            consoleUI.ConsoleStart(host, databaseContext);
        }
    }
}