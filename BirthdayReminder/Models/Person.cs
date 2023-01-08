﻿using System.ComponentModel.DataAnnotations;

namespace BirthdayReminder.Models
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
        //first und dann last
        public Person(string lastName, string firstName, DateTime birthdayDate, string email)
        {
            LastName = lastName;
            FirstName = firstName;
            BirthdayDate = birthdayDate;
            Email = email;
            Id = Guid.NewGuid();
        }
    }
}