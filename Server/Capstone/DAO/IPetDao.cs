using System.Collections.Generic;
using Vpat.Models;

namespace Vpat.DAO
{
    public interface IPetDao
	{
		Pet ReadPet(int petId);
		List<Pet> ListPets(int userId);
		Pet WritePet(NewPet newPet);
		Pet UpdatePet(Pet pet);
		bool DeletePet(int petId);
	}
}