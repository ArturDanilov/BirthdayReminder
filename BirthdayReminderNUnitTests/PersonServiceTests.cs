using BirthdayReminder.Database;
using BirthdayReminder.Models;
using BirthdayReminder.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;

namespace BirthdayReminderNUnitTests
{
    public class PersonServiceTests
    {
        //private readonly PersonService _personService;
        //public PersonServiceTests (PersonService personService) 
        //{
        //    _personService = personService;
        //}


        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetAllPersons_()
        {
            // Arrange
            //DatabaseContext databaseContext = new DatabaseContext();
            //var dependency = new PersonService(databaseContext);
            //dependency.DisplayPeopleTomorowBirthday()

            // Act

            //Assert
        }

        [Test]
        public void DisplayAllPeople_()
        {
            // Arrange

            // Act

            //Assert
        }

        [Test]
        public void DisplayPeopleTodayBirthday_()
        {
            // Arrange
            //List<Person> list = new List<Person>()
            //{
            //    new Person("Todayev", "Birthdmann", DateTime.Now, "mail.de"),
            //    new Person("Tomorovich", "Birthdmann", DateTime.Today.AddDays(1), "mail.ru")
            //};

            // Act
            //_personService.DisplayPeopleTomorowBirthday(list);

            //Assert
        }
        [Test]
        public void DisplayPeopleTomorowBirthday_()
        {
            // Arrange
            //var mockDbContext = new Mock<IDatabaseContext>();
            //IHost host = new Mock<IHost>();
            //var newPerson = new Person();
            //var iPersonService = new PersonService(mockDbContext.Object);
            //var person = new PersonService(mockDbContext.Object);
            //mockDbContext.Setup(x => x.AddPersonFromContext(newPerson));

            //List<Person> list = new List<Person>()
            //{
            //    new Person("Todayev", "Birthdmann", DateTime.Now, "mail.de"),
            //    new Person("Tomorovich", "Birthdmann", DateTime.Today.AddDays(1), "mail.ru")
            //};

            // Act
            //iPersonService.DisplayPeopleTomorowBirthday()
            //person.DisplayPeopleTodayBirthday()

            //Assert
        }

        [Test]
        public void CreateNewPerson_()
        {
            // Arrange

            // Act

            //Assert
        }
        [Test]
        public void CreatePersonBirthdayToday_()
        {
            // Arrange

            // Act

            //Assert
        }
        [Test]
        public void ReloadDb_()
        {
            // Arrange

            // Act

            //Assert
        }

        //[Test]
        //public void DisplayPeopleTodayBirthday_CallsAddPersonFromContextOne2()
        //{
        //    // Arrange           
        //    var mockDbContext = new Mock<IDatabaseContext>();
        //    Person newPerson = new Person();
        //    mockDbContext.Setup(x => x.AddPersonFromContext(newPerson));
        //    PersonService personService = new PersonService(mockDbContext.Object);

        //    // Act
        //    personService.DisplayPeopleTodayBirthday();

        //    //Assert
        //}

        [Test]
        public void PersonService_CallAddPersonFromContextOne()
        {
            // Arrange
            List<Person> list = new List<Person>()
            {
                new Person("ali", "Qattan", DateTime.Now, "hhh"),
                new Person("Mix", "Artur", DateTime.Today.AddDays(1), "hhh")
            };
            DbSet<Person> mydbSet = GetQueryableMockDbSet(list);
            var mockDbContext = new Mock<IDatabaseContext>();
            mockDbContext.Setup(x => x.People).Returns(mydbSet);
            PersonService personService = new PersonService(mockDbContext.Object);

            // Act
            var actual = personService.PeopleList();

            //Assert
            mockDbContext.Verify(x => x.People, Times.Once());
        }

        [Test]
        public void AddPersonFromService_CallsAddPersonFromContextOne()
        {
            // Arrange           
            var mockDbContext = new Mock<IDatabaseContext>();  //IDatabaseContext databasecontext = new DatabaseContext();
            Person newPerson = new Person();
            mockDbContext.Setup(x => x.AddPersonFromContext(newPerson));
            PersonService personService = new PersonService(mockDbContext.Object); //new PersonService(databasecontext)

            // Act
            personService.AddPersonFromPersonService(newPerson);

            //Assert
            mockDbContext.Verify(x => x.AddPersonFromContext(newPerson), Times.Once());
        }

        //Hier erstelle ich ein MockDbSet mit einer vorgegebenen Liste
        private static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();
            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

            return dbSet.Object;
        }
    }
}
