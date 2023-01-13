using BirthdayReminder.Database;
using BirthdayReminder.Models;
using Microsoft.Extensions.Hosting;

namespace BirthdayReminder.Services
{
    public interface IPersonService
    {
        List<Person> AllPeople();
        object CreatePerson(string? firstName, string? lastName, DateTime birthday, string? email);
        List<Person> Find29FebruarBirthday(DateTime date);
        List<Person> FindByDate(DateTime date);
        void AddPersonFromPersonService(Person person);
        void DisplayPeopleTodayBirthday(IHost host);
        void CreateNewPerson(IDatabaseContext context);
        List<Person> GetAllPersons(IServiceProvider services);
        void ReloadDb(IDatabaseContext context);
    }
}