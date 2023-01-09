using BirthdayReminder.Database;
using BirthdayReminder.Models;
using BirthdayReminder.Services;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

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

            //var peopleList = birthdayService.FindByDate(DateTime.Now("yyyy.MM.dd"));


            BirthdayService birthdayService = new BirthdayService();
            var people = birthdayService.AllPeople();

            foreach (var item in people)
            {
                Console.WriteLine($"{item.FullName} {item.BirthdayDate.ToString("d", CultureInfo.GetCultureInfo("de-DE"))}");
            }

            //CreateData();

            Console.ReadLine();
        }
        static void CreateData()
        {
            using (BirthdayContext context = new BirthdayContext())
            {
                context.Add(new Person("Ali", "Quattan", new DateTime(1970, 11, 01), "ali@adesso.com"));
                context.Add(new Person("Swen", "Elter", new DateTime(1980, 01, 01), "sven@adesso.com"));
                context.Add(new Person("Markus", "Lochner", new DateTime(1990, 01, 01), "markus@adesso.com"));
                context.Add(new Person("Artur", "Danilov", new DateTime(1960, 01, 01), "artur@adesso.com"));

                context.SaveChanges();
            }
        }
    }
}