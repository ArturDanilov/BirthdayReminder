namespace BirthdayReminder
{
    public class Person
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public DateTime BirthdayDate { get; set; }
        public string Email { get; set; }
        public Person(string name, string firstName, DateTime birthdayDate, string email)
        {
            this.Name = name;
            this.FirstName = firstName;
            this.BirthdayDate = birthdayDate;
            this.Email = email;
        }
    }
}
