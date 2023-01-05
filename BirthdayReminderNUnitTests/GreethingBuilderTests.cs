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
            Assert.AreEqual("Ali", actual);
        }

        [Test]
        public void GreeztingBilder_GreethingPersonTomorow_ExpectOneName()
        {
            // Arrange
            GreethingBuilder greethingBuilder = new GreethingBuilder();

            // Act
            string actual = greethingBuilder.GetAllPersonsNamesTomorowBirthday();

            //Assert
            Assert.AreEqual("Tom", actual);
        }
    }
}