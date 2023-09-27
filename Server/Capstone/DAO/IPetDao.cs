using System.Collections.Generic;
using Vpat.Models;

namespace Vpat.DAO
{
    public interface IPetDao
	{
		int CountPets(int userId);
		Pet ReadPet(int petId);
		List<Pet> ListPets(int userId); // TODO: SORT BY in query string!!
		Pet WritePet(NewPet pet);
		bool DeletePet(int petId);
	}
}

