using System;

namespace Vpat.Models
{
    public class Pet
    {
        // Properties
        public string Name { get; private set; }
        public string Type { get; private set; }
        private string DateBirth { get; set; }
        private string TimeBirth { get; set; }
        public DateTime Birthday
        {
            get
            {
                DateOnly date = DateOnly.Parse(DateBirth);

                TimeOnly time = TimeOnly.Parse(TimeBirth);

                string combinedDateTime = $"{date} {time}";

                DateTime birthday = DateTime.Parse(combinedDateTime);

                return birthday;
            }
        }

        // Constructor
        public Pet(string name, string type, string dateBirth, string timeBirth)
        {
            Name = name;
            Type = type;
            DateBirth = dateBirth;
            TimeBirth = timeBirth;
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