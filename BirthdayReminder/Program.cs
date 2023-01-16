using BirthdayReminder.Database;
using BirthdayReminder.Services;
using BirthdayReminder.Telegram;
using BirthdayReminder.UI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Hosting;

namespace BirthdayReminder
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Зависимость регистрируется в контейнере служб
            //A host is an object that encapsulates an app's resources, such as:
            //- Dependency injection(DI)
            //- Logging
            //- Configuration
            //- IHostedService implementations
            
            using IHost host = Host
            .CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddDbContext<DatabaseContext>() //return new DatabasContext();
                        .AddScoped<IPersonService, PersonService>() //AddScoped службы создаются один раз для каждого запроса (подключения) клиента
                        .AddSingleton<ITelegramService, TelegramService>()//singelton работает столько сколько работает сервис
                        .AddSingleton<IDatabaseContext>(provider => provider.GetService<DatabaseContext>());
            })
            .Build();

            IDatabaseContext databaseContext = host.Services.GetRequiredService<IDatabaseContext>(); //?

            var consoleUI = new ConsoleUI();
            consoleUI.ConsoleStart(host, databaseContext);
        }
    }
}