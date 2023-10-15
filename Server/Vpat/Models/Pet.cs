using System;
using System.Globalization;

namespace Vpat.Models
{
    public class Pet
    {
        private readonly TimeSolver timeSolver = new TimeSolver();

        public int PetId { get; set; }
        public string PetName { get; set; }
        public string PetType { get; set; }
        public string Brand { get; set; }
        public string DateBirth { get; set; }
        public string TimeBirth { get; set; }
        public string DateDeath { get; set; }
        public bool IsHidden { get; set; }
        public int UserId { get; set; }
        public string Birthday
        {
            get
            {
                DateTime birthday = timeSolver.ParseDateTime(DateBirth, TimeBirth);
                string birthdayString = birthday.ToString("g", CultureInfo.CreateSpecificCulture("en-us"));

                return birthdayString;
            }
        }
        public string Age
        {
            get
            {
                DateTime birthday = timeSolver.ParseDateTime(DateBirth, TimeBirth);

                if (DateDeath == null)
                {

                    DateTime currentDate = DateTime.Now;
                    TimeSpan currentAge = currentDate.Subtract(birthday);

                    int ageInDays = currentAge.Days;
                    string estimatedAge = timeSolver.YearCount(ageInDays);

                    return estimatedAge;
                }
                else // DateDeath != null
                {
                    DateTime deathDay = timeSolver.ParseDateTime(DateDeath, "12:00:00 AM");
                    TimeSpan deathAge = deathDay.Subtract(birthday);

                    int ageInDays = deathAge.Days;
                    string estimatedAge = timeSolver.YearCount(ageInDays);

                    return estimatedAge;
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