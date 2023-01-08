using BirthdayReminder.Models;

namespace BirthdayReminder
{
    //TODO umbenenen
    public class GreethingBuilder
    {
        List<Person> people = new List<Person>()
        {
            new Person("Ali", "Quattan", DateTime.Today, "ali@adesso.com"),
            new Person("Swen", "Elter", DateTime.Today.AddDays(+1), "sven@adesso.com"),
            new Person("Markus", "Lochner", DateTime.Today.AddDays(+2), "markus@adesso.com"),
            new Person("Artur", "Danilov", DateTime.Today.AddDays(+3), "artur@adesso.com"),
        };

        public List<string> GetAllPersonsWithTodayBirthday()
        {
            var todayBirthday = people
                .Where(x => x.BirthdayDate.Month == DateTime.Today.Month && x.BirthdayDate.Day == DateTime.Today.Day)
                .Select(x => "Schidiger: " + x.LastName + " " + x.FirstName + "\nBirthday: " + x.BirthdayDate + "\nMail: " + x.Email)
                .ToList();

            return todayBirthday;
        }
        public string GetAllPersonsNamesTodayBirthday()
        {
            string namesOfPerson = "";

            foreach (var item in GetAllPersonsWithTodayBirthday())
                namesOfPerson += item;

            return namesOfPerson;
        }
        public List<string> GetAllPersonsWithTomorowBirthday()
        {
            var tomorowBirthday = people
                .Where(x => x.BirthdayDate.Month == DateTime.Today.Month && x.BirthdayDate.Day == DateTime.Today.Day + 1)
                .Select(x => "Schidiger: " + x.LastName + " " + x.FirstName + "\nBirthday: " + x.BirthdayDate + "\nMail: " + x.Email)
                .ToList();

            return tomorowBirthday;
        }
        public string GetAllPersonsNamesTomorowBirthday()
        {
            string namesOfPersonTomorow = "";

            foreach (var item in GetAllPersonsWithTomorowBirthday())
                namesOfPersonTomorow += item;

            return namesOfPersonTomorow;
        }
    }
}
