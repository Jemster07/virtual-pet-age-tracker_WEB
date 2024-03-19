using System;
namespace Vpat.Models
{
	public class TimeSolver
	{
        public string YearCount(int ageInDays)
        {
            string estimatedAge = "";

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