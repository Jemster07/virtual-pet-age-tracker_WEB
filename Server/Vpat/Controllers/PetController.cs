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
				return Ok(petDao.WritePet(newPet));
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpPut("update/{petId}")]
		public ActionResult<Pet> UpdatePet(Pet pet)
		{
			try
			{				
				string[] dateArray = pet.Birthday.Split(' ');
				string dateBirth = dateArray[0];

				pet.DateBirth = dateBirth;

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
    }
}