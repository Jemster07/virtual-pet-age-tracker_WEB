using System.Collections.Generic;
using Vpat.Models;

namespace Vpat.DAO
{
    public interface IPetDao
	{
		Pet ReadPet(int petId);
		List<Pet> ListPets(int userId);
		Pet WritePet(Pet Pet);
		Pet UpdatePet(Pet pet);
		bool DeactivatePet(int petId);
		bool DeletePets();
	}
}