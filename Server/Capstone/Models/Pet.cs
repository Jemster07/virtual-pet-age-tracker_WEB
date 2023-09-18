using System;
namespace Vpat.Models
{
    public abstract class Pet
    {
        // Properties
        public string Name { get; private set; }
        public string Type { get; private set; }

        // Constructor
        public Pet(string name, string type)
        {
            Name = name;
            Type = type;
        }

        // Method
        public TimeSpan CalculateAge(DateTime birthday)
        {
            DateTime currentDate = DateTime.Now;
            TimeSpan age = currentDate.Subtract(birthday);

            return age;
        }
    }
}