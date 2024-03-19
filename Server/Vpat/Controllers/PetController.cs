using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vpat.DAO;
using Vpat.Models;
namespace Vpat.Controllers
{
	[Route("[controller]")]
	[ApiController]
	[Authorize]
	public class PetController : ControllerBase
	{
		private readonly IPetDao petDao;

		public PetController(IPetDao _petDao)
		{
			petDao = _petDao;
		}

		[HttpGet("{petId}")]
		public ActionResult<Pet> GetPet(int petId)
		{
			try
			{
				Pet pet = petDao.ReadPet(petId);

				if (pet.IsHidden)
				{
					return Unauthorized("Pet has been deactivated.");
				}
				else
				{
					return Ok(pet);	
				}
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpGet("/{userId}")]
		public ActionResult<List<Pet>> ListPets(int userId)
		{
			try
			{
				List<Pet> petList = petDao.ListPets(userId);

				if (petList.Count <= 0)
				{
					return NoContent();
				}
				else
				{
					return Ok(petList);
				}
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpPost("add")]
		public ActionResult<Pet> AddPet(NewPet newPet)
		{
			try
			{
				Pet pet = NewPetMapper(newPet);
				
				return Ok(petDao.WritePet(pet));
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpPut("update/{petId}")]
		public ActionResult<Pet> UpdatePet(EditPet editPet)
		{
			try
			{				
				Pet pet = EditPetMapper(editPet);
				
				return Ok(petDao.UpdatePet(pet));
			}
			catch (Exception)
			{
				return StatusCode(500, "Error updating pet information.");
			}
		}

		[HttpDelete("deactivate/{petId}")]
		public ActionResult<bool> DeactivatePet(int petId)
		{
			try
			{
				bool petDeleted = petDao.DeactivatePet(petId);

                if (petDeleted)
				{
					return Ok(petDeleted);
				}
				else
				{
					return StatusCode(StatusCodes.Status503ServiceUnavailable, "Pet was not deleted!");
                }
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

        [Authorize(Roles = "admin")]
		[HttpDelete("delete/pets")]
		public ActionResult<bool> DeletePets()
		{
			try
			{
				bool petDeleted = petDao.DeletePets();

                if (petDeleted)
				{
					return Ok(petDeleted);
				}
				else
				{
					return StatusCode(StatusCodes.Status503ServiceUnavailable, "Pets were not deleted!");
                }
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		public Pet EditPetMapper(EditPet editPet)
		{
			DateOnly date = DateOnly.Parse(editPet.DateBirth);
            TimeOnly time = TimeOnly.Parse(editPet.TimeBirth);
            string dateTimeString = $"{date} {time}";
            DateTime editPetBirthday = DateTime.Parse(dateTimeString).ToUniversalTime();

			DateTime? editPetDeath = null;

			if (editPet.DateDeath != "" && editPet.DateDeath != null)
			{
				editPetDeath = DateTime.Parse(editPet.DateDeath).ToUniversalTime();
			}
			
			Pet pet = new Pet
			{
				PetId = editPet.PetId,
				PetName = editPet.PetName,
				PetType = editPet.PetType,
				Brand = editPet.Brand,
				Birthday = editPetBirthday,
				DateDeath = editPetDeath,
				IsHidden = editPet.IsHidden,
				UserId = editPet.UserId
			};

			return pet;
		}

		public Pet NewPetMapper(NewPet newPet)
		{
			DateOnly date = DateOnly.Parse(newPet.DateBirth);
            TimeOnly time = TimeOnly.Parse(newPet.TimeBirth);
            string dateTimeString = $"{date} {time}";
            DateTime newPetBirthday = DateTime.Parse(dateTimeString).ToUniversalTime();

			Pet pet = new Pet
			{
				PetId = 0,
				PetName = newPet.PetName,
				PetType = newPet.PetType,
				Brand = newPet.Brand,
				Birthday = newPetBirthday,
				DateDeath = null,
				IsHidden = false,
				UserId = newPet.UserId
			};

			return pet;
		}
    }
}