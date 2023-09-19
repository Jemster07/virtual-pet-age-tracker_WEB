using System;
namespace Vpat.Models
{
    public class Pet
    {
        // Properties
        public string PetName { get; set; }
        public string PetType { get; set; }
        public string Brand { get; set; }
        public string DateBirth { get; set; }
        public string TimeBirth { get; set; }
        public bool IsActive { get; set; }
        public bool IsHidden { get; set; }
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
        public int UserId { get; set; }

        // Constructor
        public Pet(string petName, string petType, string brand, string dateBirth,
            string timeBirth, bool isActive, bool isHidden, int userId)
        {
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

            IsActive = isActive;
            IsHidden = isHidden;
            UserId = userId;
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