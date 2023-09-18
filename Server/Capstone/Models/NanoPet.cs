using System;
namespace Vpat.Models
{
	public class NanoPet : Pet
	{
        // Properties
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
        public NanoPet(string name, string type, string dateBirth, string timeBirth)
            : base(name, type)
        {
            DateBirth = dateBirth;
            TimeBirth = timeBirth;
        }
    }
}