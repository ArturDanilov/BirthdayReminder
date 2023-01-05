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

            // Act
            string actual = greethingBuilder.GetAllPersonsNamesTodayBirthday();

            //Assert
            Assert.AreEqual("Schidiger: Ali Quattan\nBirthday: 05.01.2023 00:00:00\nMail: ali@adesso.com", actual);
        }

        [Test]
        public void GreeztingBilder_GreethingPersonTomorow_ExpectOneName()
        {
            // Arrange
            GreethingBuilder greethingBuilder = new GreethingBuilder();

            // Act
            string actual = greethingBuilder.GetAllPersonsNamesTomorowBirthday();

            //Assert
            Assert.AreEqual("Schidiger: Swen Elter\nBirthday: 06.01.2023 00:00:00\nMail: sven@adesso.com", actual);
        }
    }
}