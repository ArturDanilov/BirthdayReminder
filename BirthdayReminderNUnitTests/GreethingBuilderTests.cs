using BirthdayReminder;
using BirthdayReminder.Models;

namespace BirthdayReminderNUnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GreeztingBilder_GreethingPersonToday_ExpectOneName()
        {
            // Arrange
            PersonBuilder greethingBuilder = new PersonBuilder();
            DateTime birthDate = DateTime.Today;

            // Act
            string actual = greethingBuilder.GetAllPersonsNamesTodayBirthday();

            //Assert
            Assert.AreEqual($"Schidiger: Ali Quattan\nBirthday: {birthDate}\nMail: ali@adesso.com", actual);
        }

        [Test]
        public void GreeztingBilder_GreethingPersonTomorow_ExpectOneName()
        {
            // Arrange
            PersonBuilder greethingBuilder = new PersonBuilder();
            DateTime birthDate = DateTime.Today.AddDays(+1);

            // Act
            string actual = greethingBuilder.GetAllPersonsNamesTomorowBirthday();

            //Assert
            Assert.AreEqual($"Schidiger: Swen Elter\nBirthday: {birthDate}\nMail: sven@adesso.com", actual);
        }

        //TODO Fullname
        [Test]
        public void GreeztingBilder_ShowFullName_ExpectFirsNameAndLastNameReturnFirstNamePlusLastName()
        {
            // Arrange
            Person person = new Person()
            {
                FirstName = "Anna",
                LastName = "Conda"
            };

            // Act
            string actual = $"{person.FirstName} {person.LastName}";

            //Assert
            Assert.That(actual, Is.EqualTo(person.FullName));
        }

    }
}