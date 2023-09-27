using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Vpat.DAO;
using Vpat.Models;
namespace Vpat.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class PetController : ControllerBase
	{
		private readonly IPetDao petDao;

		public PetController(IPetDao _petDao)
		{
			petDao = _petDao;
		}

		// TODO: If petList.Count == 0, return a warning in Controller
		// TODO: Remap Pet objects to ReturnPet objects

		[HttpGet("{petId}")]
		public ActionResult<Pet> GetPet(int petId)
		{
			try
			{
				Pet pet = petDao.ReadPet(petId);

				if (pet.IsHidden)
				{
					return Unauthorized("Pet has been deactivated");
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
			return StatusCode(500);
		}
    }
}

