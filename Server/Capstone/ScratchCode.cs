using System;
using Vpat.Models;
namespace Vpat
{
	public class ScratchCode
	{
		public OtherPet RunTest()
		{
			string name = "Simon";
			string type = "Test Pet";
			string dateBirth = "09/18/2023";
			string timeBirth = null;
			bool ageChime = false;

			OtherPet testPet = new OtherPet(name, type, dateBirth, timeBirth, ageChime);

			return testPet;
		}
	}
}

