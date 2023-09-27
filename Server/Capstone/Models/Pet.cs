using System;
namespace Vpat.Models
{
    public class Pet
    {
        // Properties
        public int PetId { get; set; }
        public string PetName { get; set; }
        public string PetType { get; set; }
        public string Brand { get; set; }
        public string DateBirth { get; set; }
        public string TimeBirth { get; set; }
        public string DateDeath { get; set; }
        public bool IsActive { get; set; }
        public bool IsHidden { get; set; }
        public int UserId { get; set; }
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
        public DateTime DeathDay
        {
            get
            {
                DateOnly date = DateOnly.Parse(DateDeath);

                string combinedDateTime = $"{date} 12:00:00 AM";

                DateTime deathDay = DateTime.Parse(combinedDateTime);

                return deathDay;
            }
        }

        // Methods
        public TimeSpan CalculateAge(DateTime birthday)
        {
            DateTime currentDate = DateTime.Now;
            TimeSpan age = currentDate.Subtract(birthday);

            return age;
        }

        public TimeSpan CalculateDeathAge(DateTime birthday, DateTime deathDay)
        {
            TimeSpan deathAge = deathDay.Subtract(birthday);

            return deathAge;
        }
    }

    public class ReturnPet
    {
        public string PetName { get; set; }
        public string PetType { get; set; }
        public string Brand { get; set; }
        public bool IsActive { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime DeathDay { get; set; }
        public TimeSpan CurrentAge { get; set; }
        public TimeSpan DeathAge { get; set; }
    }

    public class NewPet
    {
        public string PetName { get; set; }
        public string PetType { get; set; }
        public string Brand { get; set; }
        public string DateBirth { get; set; }
        public string TimeBirth { get; set; }
        public int UserId { get; set; }
    }
}