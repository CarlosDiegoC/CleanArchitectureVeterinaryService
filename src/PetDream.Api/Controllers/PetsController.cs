using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetDream.Application.DTOs;
using PetDream.Application.Interfaces;

namespace PetDream.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "Veterinary")]
    public class PetsController : ControllerBase
    {   
        private readonly IPetService _petService;
        private readonly IPetOwnerService _petOwnerService;
        public PetsController(IPetService petService, IPetOwnerService petOwnerService)
        {
            _petService = petService;
            _petOwnerService = petOwnerService;
        }

        /// <summary>
        /// Get the full list of active pets.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var pets = await _petService.Get();
            return Ok(pets);
        }

        /// <summary>
        /// Get a pet by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pet = await _petService.GetById(id);
            return Ok(pet);
        }

        /// <summary>
        /// Add a new pet.
        /// </summary>
        /// <param name="newPetDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post(NewPetDTO newPetDTO)
        {
            try
            {
                if(newPetDTO == null) return BadRequest("Invalid data.");
                var petOwner = await _petOwnerService.GetById(newPetDTO.PetOwnerId);
                if(petOwner is null) return BadRequest("This pet owner does not exists.");
                await _petService.Add(newPetDTO);
                return Ok(newPetDTO);
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
            
        }

        /// <summary>
        /// Update an existing pet entering with complete informations.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatePetDTO"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, UpdatePetDTO updatePetDTO)
        {
            if(id != updatePetDTO.Id) return BadRequest("Invalid data.");
            if(updatePetDTO == null) return NotFound("Pet not found.");
            await _petService.Update(updatePetDTO);
            return Ok(updatePetDTO);
        }

        /// <summary>
        /// Deactive a pet registration (Boolean Deletion).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var petDTO = await _petService.GetById(id);
            if(petDTO == null) return NotFound("Product not found.");
            await _petService.Remove(id);
            return Ok(petDTO);
        }
    }
}