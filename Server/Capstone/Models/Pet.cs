using System;
namespace Vpat.Models
{
    public class Pet
    {
        public int PetId { get; set; }
        public string PetName { get; set; }
        public string PetType { get; set; }
        public string Brand { get; set; }
        public string DateBirth { get; set; }
        public string TimeBirth { get; set; }
        public string DateDeath { get; set; }
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
        public TimeSpan Age
        {
            get
            {
                if (DateDeath == null)
                {
                    DateTime currentDate = DateTime.Now;
                    TimeSpan currentAge = currentDate.Subtract(Birthday);

                    return currentAge;
                }
                else // DateDeath != null
                {
                    DateOnly dateDeath = DateOnly.Parse(DateDeath);
                    string combinedDateTime = $"{dateDeath} 12:00:00 AM";
                    DateTime deathDay = DateTime.Parse(combinedDateTime);
                    TimeSpan deathAge = deathDay.Subtract(Birthday);

                    return deathAge;
                }
            }
        }
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