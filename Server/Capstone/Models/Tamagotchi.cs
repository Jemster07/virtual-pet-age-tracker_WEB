using System;

namespace Vpat.Models
{
	public class Tamagotchi : Pet
	{
        // Constructor
        public Tamagotchi(string name, string type, string dateBirth, string timeBirth)
            : base(name, type, dateBirth, timeBirth) { }
    }
}

