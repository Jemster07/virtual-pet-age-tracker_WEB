using System;

namespace Vpat.Models
{
    public class Pet
    {
        private readonly TimeSolver timeSolver = new TimeSolver();

        public int PetId { get; set; }
        public string PetName { get; set; }
        public string PetType { get; set; }
        public string Brand { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime? DateDeath { get; set; }
        public bool IsHidden { get; set; }
        public int UserId { get; set; }
        public string Age
        {
            get
            {
                if (DateDeath == null)
                {

                    DateTime currentDate = DateTime.Now;
                    TimeSpan currentAge = currentDate.Subtract(Birthday);

                    int ageInDays = currentAge.Days;
                    string estimatedAge = timeSolver.YearCount(ageInDays);

                    return estimatedAge;
                }
                else // DateDeath != null
                {
                    DateTime deathDay = (DateTime)DateDeath;
                    TimeSpan deathAge = deathDay.Subtract(Birthday);

                    int ageInDays = deathAge.Days;
                    string estimatedAge = timeSolver.YearCount(ageInDays);

                    return estimatedAge;
                }
            }
        }
    }

    public class ViewPet
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
    }
}