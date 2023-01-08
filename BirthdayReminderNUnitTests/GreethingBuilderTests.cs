using BirthdayReminder;

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
            GreethingBuilder greethingBuilder = new GreethingBuilder();
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
            GreethingBuilder greethingBuilder = new GreethingBuilder();
            DateTime birthDate = DateTime.Today.AddDays(+1);

            // Act
            string actual = greethingBuilder.GetAllPersonsNamesTomorowBirthday();

            //Assert
            Assert.AreEqual($"Schidiger: Swen Elter\nBirthday: {birthDate}\nMail: sven@adesso.com", actual);
        }

        //TODO Fullname
    }
}