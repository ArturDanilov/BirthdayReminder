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

            //using IHost host = Host
            //.CreateDefaultBuilder(args)
            //.ConfigureServices(services =>
            //{
            //    services.AddDbContext<DatabaseContext>() //return new DatabasContext();
            //            .AddScoped<IPersonService, PersonService>() //AddScoped - Services are created once for each client request (connection)
            //            .AddSingleton<ITelegramService, TelegramService>()//singelton - works as long as the service works
            //            .AddSingleton<IDatabaseContext>(provider => provider.GetService<DatabaseContext>());
            //})
            //.Build();

            //IDatabaseContext databaseContext = host.Services.GetRequiredService<IDatabaseContext>();

            //var consoleUI = new ConsoleUI();
            //consoleUI.ConsoleStart(host, databaseContext);


            var servicesNoHost = new ServiceCollection()
                .AddDbContext<DatabaseContext>()
                .AddSingleton<IDatabaseContext>(provider => provider.GetService<DatabaseContext>())
                .AddSingleton<ITelegramService, TelegramService>()
                .AddSingleton<IPersonService, PersonService>()
                .BuildServiceProvider();

            var dbContext = servicesNoHost.GetRequiredService<IDatabaseContext>();

            var consoleUI = new ConsoleUI_IServiceProvider();
            consoleUI.ConsoleStart(servicesNoHost, dbContext);
        }
    }
}