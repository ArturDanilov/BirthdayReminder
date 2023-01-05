using System.ComponentModel.DataAnnotations;

namespace BirthdayReminder
{
    public class Person
    {
        [Key]
        public Guid Id { get; set; }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime BirthdayDate { get; set; }
        public string Email { get; set; }
        public string FullName => $"{FirstName} {LastName}"; //TODO testen
        public Person(string lastName, string firstName, DateTime birthdayDate, string email)
        {
            this.LastName = lastName;
            this.FirstName = firstName;
            this.BirthdayDate = birthdayDate;
            this.Email = email;
            this.Id= Guid.NewGuid();
        }
    }
}
