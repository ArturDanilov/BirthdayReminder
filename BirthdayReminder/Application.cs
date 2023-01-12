using BirthdayReminder.Database;
using BirthdayReminder.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayReminder
{
    public class Application
    {
        IPersonService _personService;
        public Application(IPersonService personService)
        {
            _personService= personService;   
        }

        private void IdentifyNextStep()
        {    
            string selectedAction = "";

            do
            {
                selectedAction = GetActionChoise();
                Console.WriteLine();

                switch (selectedAction)
                {
                    case "1":
                        DisplayPeople();
                        break;
                    case "2":
                        AddPerson();
                        break;
                    case "3":
                        Console.WriteLine("Chiao");
                        break;
                    default:
                        Console.WriteLine("Hit enter und try again!");
                        break;
                }

                Console.WriteLine("Hit return to continue...");
                Console.ReadLine();

            } while (selectedAction != "3");
        }

        private string GetActionChoise()
        {
            throw new NotImplementedException();
        }

        private void DisplayPeople()
        {
            throw new NotImplementedException();
        }

        private void AddPerson()
        {
            Console.WriteLine("First name...");
            string firstName = Console.ReadLine();
            Console.WriteLine("Last name...");
            string lastName = Console.ReadLine();
            Console.WriteLine("Birthday...");
            DateTime birthday = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Email...");
            string email = Console.ReadLine();

            var person = _personService.CreatePerson(firstName, lastName, birthday, email);
            //_personService.AddPersonFromPersonService((Person)person);
        }
    }
}
