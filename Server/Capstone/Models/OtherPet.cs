using System;
namespace Vpat.Models
{
	public class OtherPet : Pet
	{
        // Properties
        private string DateBirth { get; set; }
        private string TimeBirth { get; set; }
        public bool AgeChime { get; set; }
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
        public OtherPet(string name, string type, string dateBirth, string timeBirth, bool ageChime)
            : base(name, type)
        {
            DateBirth = dateBirth;
            AgeChime = ageChime;

            if (!AgeChime)
            {
                TimeBirth = "12:00:00 AM";
            }
            else
            {
                TimeBirth = timeBirth;
            }
        }
    }
}