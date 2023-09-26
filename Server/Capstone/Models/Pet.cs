using System;
namespace Vpat.Models
{
    public class Pet
    {
        //TODO: Create models for different tasks

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

        // Constructor
        public Pet(int petId, string petName, string petType, string brand, string dateBirth,
            string timeBirth, string dateDeath, bool isActive, bool isHidden, int userId)
        {
            PetId = petId;
            PetName = petName;
            PetType = petType;
            Brand = brand;
            DateBirth = dateBirth;

            if (timeBirth == null)
            {
                TimeBirth = "12:00:00 AM";
            }
            else
            {
                TimeBirth = timeBirth;
            }

            DateDeath = dateDeath;
            IsActive = isActive;
            IsHidden = isHidden;
            UserId = userId;
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
}