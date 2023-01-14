using System.ComponentModel.DataAnnotations;

namespace BirthdayReminder.Models
{
    public class Person
    {
        [Key]
        public Guid Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthdayDate { get; set; }
        public string Email { get; set; }
        public string FullName => $"{LastName} {FirstName}"; //!TODO testen

        public Person( string firstName,string lastName, DateTime birthdayDate, string email) //!TODO first und dann last
        {
            FirstName = firstName;
            LastName = lastName;
            BirthdayDate = birthdayDate;
            Email = email;
            Id = Guid.NewGuid();
        }
        public Person() 
        {
        } 

    }
}
