using System;

namespace Vpat.Models
{
    public class GigaPet : Pet
    {
        // Constructor
        public GigaPet(string name, string type, string dateBirth, string timeBirth = "00:00:00")
            : base(name, type, dateBirth, timeBirth) { }
    }
}