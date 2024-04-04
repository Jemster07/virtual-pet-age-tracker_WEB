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
		public ActionResult<Pet> AddPet(ViewPet viewPet)
		{
			try
			{
				Pet pet = ViewPetMapper(viewPet);
				
				return Ok(petDao.WritePet(pet));
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpPut("update/{petId}")]
		public ActionResult<Pet> UpdatePet(ViewPet viewPet)
		{
			try
			{				
				Pet pet = ViewPetMapper(viewPet);
				
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

		protected Pet ViewPetMapper(ViewPet viewPet)
		{
			DateOnly date = DateOnly.Parse(viewPet.DateBirth);
            TimeOnly time = TimeOnly.Parse(viewPet.TimeBirth);
            string dateTimeString = $"{date} {time}";
            DateTime editPetBirthday = DateTime.Parse(dateTimeString);

			DateTime? editPetDeath = null;

			if (viewPet.DateDeath != "" && viewPet.DateDeath != null)
			{
				editPetDeath = DateTime.Parse(viewPet.DateDeath);
			}
			
			Pet pet = new Pet
			{
				PetId = viewPet.PetId,
				PetName = viewPet.PetName,
				PetType = viewPet.PetType,
				Brand = viewPet.Brand,
				Birthday = editPetBirthday,
				DateDeath = editPetDeath,
				IsHidden = viewPet.IsHidden,
				UserId = viewPet.UserId
			};

			return pet;
		}
    }
}