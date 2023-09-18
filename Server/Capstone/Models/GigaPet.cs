﻿using System;
namespace Vpat.Models
{
    public class GigaPet : Pet
    {
        // Properties
        private string DateBirth { get; set; }
        public DateTime Birthday
        {
            get
            {
                DateOnly date = DateOnly.Parse(DateBirth);

                string combinedDateTime = $"{date} 12:00:00 AM";

                DateTime birthday = DateTime.Parse(combinedDateTime);

                return birthday;
            }
        }

        // Constructor
        public GigaPet(string name, string type, string dateBirth)
            : base(name, type)
        {
            DateBirth = dateBirth;
        }
    }
}