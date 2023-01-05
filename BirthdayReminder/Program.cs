using BirthdayReminder.Database;

namespace BirthdayReminder
{
    public class Program
    {
        static void Main(string[] args)
        {
            //GreethingBuilder greethingService = new GreethingBuilder();

            //Console.WriteLine("Today:\n");
            //var birthdaySchuldigerToday = greethingService.GetAllPersonsWithTodayBirthday();
            //foreach (var item in birthdaySchuldigerToday)
            //{
            //    Console.WriteLine(item + "\n");
            //}

            //Console.WriteLine("Tomorow:\n");

            //var birthdaySchuldigerTomorow = greethingService.GetAllPersonsWithTomorowBirthday();
            //foreach (var item in birthdaySchuldigerTomorow)
            //{
            //    Console.WriteLine(item + "\n");
            //}

            using (BirthdayContext context = new BirthdayContext())
            {
                context.Add(new Person("Ali", "Quattan", new DateTime(1970, 11, 01), "ali@adesso.com"));
                context.Add(new Person("Swen", "Elter", new DateTime(1980, 01, 01), "sven@adesso.com"));
                context.Add(new Person("Markus", "Lochner", new DateTime(1990, 01, 01), "markus@adesso.com"));
                context.Add(new Person("Artur", "Danilov", new DateTime(1960, 01, 01), "artur@adesso.com"));
                context.SaveChanges();
            }

            Console.ReadLine();
        }
    }
}