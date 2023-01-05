namespace BirthdayReminder
{
    public class Program
    {
        static void Main(string[] args)
        {
            GreethingBuilder greethingService = new GreethingBuilder();

            Console.WriteLine("Today:\n");
            var birthdaySchuldigerToday = greethingService.GetAllPersonsWithTodayBirthday();
            foreach (var item in birthdaySchuldigerToday)
            {
                Console.WriteLine(item + "\n");
            }

            Console.WriteLine("Tomorow:\n");

            var birthdaySchuldigerTomorow = greethingService.GetAllPersonsWithTomorowBirthday();
            foreach (var item in birthdaySchuldigerTomorow)
            {
                Console.WriteLine(item + "\n");
            }

            Console.ReadLine();
        }
    }
}