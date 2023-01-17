using BirthdayReminder.Database;
using BirthdayReminder.Services;
using BirthdayReminder.Telegram;

namespace BirthdayReminder.UI
{
    public class ConsoleUI_IServiceProvider
    {
        public void ConsoleStart(IServiceProvider provider, IDatabaseContext context)
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
                "\n8 - Wer ist 29.02 geboren?" +
                "\n0 - App schließen" +
                "\n\nDeine Auswahl ist...");

            var selectedAction = "";
            do
            {
                selectedAction = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                var service = new PersonService_IServiceProvider(context);

                switch (selectedAction)
                {
                    case "1":
                        service.DisplayAllPeople(provider);
                        break;
                    case "2":
                        service.CreateNewPerson(context);
                        break;
                    case "3":
                        service.ReloadDb(context);
                        break;
                    case "4":
                        var telegramService = new TelegramService();
                        telegramService.CallTelegramBot();
                        break;
                    case "5":
                        service.CreatePersonBirthdayToday(context); //TODO: tempo.daten
                        service.DisplayPeopleTodayBirthday(provider);
                        break;
                    case "6":
                        service.CreatePersonBirthdayTomorow(context); //TODO: tempo.daten
                        service.DisplayPeopleTomorowBirthday(provider);
                        break;
                    case "7":
                        Console.WriteLine("\nWollen wir mit Hilfe chat.openai um jemandem zu gratulieren?\n\ny - yes\nn - no");
                        var antwort = Console.ReadLine();
                        switch (antwort)
                        {
                            case "y":
                                System.Threading.Thread.Sleep(1000);
                                Console.WriteLine("Du kannst chat.openai nutzen");
                                service.ChatOpenai();
                                break;
                            case "n":
                                Console.WriteLine("Service kapput ;) Sapß Du kannst weiter machen");
                                break;
                            default:
                                break;
                        }
                        break;
                    case "8":
                        service.Find29Februar(provider);
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
    }
}
