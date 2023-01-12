using BirthdayReminder.Models;

namespace BirthdayReminder.Services
{
    public interface IPersonService
    {
        List<Person> AllPeople();
        object CreatePerson(string? firstName, string? lastName, DateTime birthday, string? email);
        List<Person> Find29FebruarBirthday(DateTime date);
        List<Person> FindByDate(DateTime date);
        void AddPersonFromPersonService(Person person);
    }
}