using System;
using System.Globalization;

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
        public string Birthday
        {
            get
            {
                DateOnly date = DateOnly.Parse(DateBirth);
                TimeOnly time = TimeOnly.Parse(TimeBirth);
                string combinedDateTime = $"{date} {time}";
                DateTime birthday = DateTime.Parse(combinedDateTime);

                string birthdayString = birthday.ToString("g", CultureInfo.CreateSpecificCulture("en-us"));

                return birthdayString;
            }
        }
        public string Age
        {
            get
            {
                DateOnly date = DateOnly.Parse(DateBirth);
                TimeOnly time = TimeOnly.Parse(TimeBirth);
                string combinedDateTime = $"{date} {time}";
                DateTime birthday = DateTime.Parse(combinedDateTime);

                string estimatedAge = "";

                if (DateDeath == null)
                {
                    DateTime currentDate = DateTime.Now;
                    TimeSpan currentAge = currentDate.Subtract(birthday);

                    int ageInDays = currentAge.Days;

                    if (ageInDays >= 365)
                    {
                        int yearCounter = 0;

                        while (ageInDays >= 365)
                        {
                            yearCounter++;
                            ageInDays -= 365;
                        }

                        if (ageInDays > 0)
                        {
                            estimatedAge = $"{yearCounter} year(s) and {ageInDays} day(s) old";
                        }
                        else
                        {
                            estimatedAge = $"{yearCounter} year(s) old";
                        }
                    }
                    else
                    {
                        estimatedAge = $"{ageInDays} day(s) old";
                    }

                    return estimatedAge;
                }
                else // DateDeath != null
                {
                    DateOnly dateDeath = DateOnly.Parse(DateDeath);
                    string combinedDeathDateTime = $"{dateDeath} 12:00:00 AM";
                    DateTime deathDay = DateTime.Parse(combinedDeathDateTime);
                    TimeSpan deathAge = deathDay.Subtract(birthday);

                    int ageInDays = deathAge.Days;

                    if (ageInDays >= 365)
                    {
                        int yearCounter = 0;

                        while (ageInDays >= 365)
                        {
                            yearCounter++;
                            ageInDays -= 365;
                        }

                        if (ageInDays > 0)
                        {
                            estimatedAge = $"{yearCounter} year(s) and {ageInDays} day(s) old";
                        }
                        else
                        {
                            estimatedAge = $"{yearCounter} year(s) old";
                        }
                    }
                    else
                    {
                        estimatedAge = $"{ageInDays} day(s) old";
                    }

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