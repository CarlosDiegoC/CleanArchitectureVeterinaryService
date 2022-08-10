using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetDream.Api.Models;
using PetDream.Application.DTOs;
using PetDream.Application.Interfaces;
using PetDream.Domain.Account;

namespace PetDream.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "Veterinary")]
    public class VeterinariansController : ControllerBase
    {
        private readonly IVeterinarianService _veterinarianService;
        private readonly IAuthenticate _authentication;

        public VeterinariansController(IVeterinarianService veterinarianService, IAuthenticate authentication)
        {
            _veterinarianService = veterinarianService;
            _authentication = authentication;
        }

        /// <summary>
        /// Get the full list of active veterinarians.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var veterinarians = await _veterinarianService.GetAsync();
            if(!veterinarians.Any()) return BadRequest("There is no veterinarians registered");
            return Ok(veterinarians);
        }

        /// <summary>
        /// Get a veterinarian by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var veterinarian = await _veterinarianService.GetByIdAsync(id);
            if(veterinarian is null) return NotFound("There is no veterinarian with this id");
            return Ok(veterinarian);
        }

        
        /// <summary>
        /// Get a veterinarian by registration.
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        [HttpGet("register/{registration}")]
        public async Task<IActionResult> GetByRegistration(string registration)
        {
            var veterinarian = await _veterinarianService.GetByRegistrationAsync(registration);
            if(veterinarian is null) return NotFound("There is no veterinarian with this register");
            return Ok(veterinarian);
        }

        /// <summary>
        /// Add a new veterinarian. Validations: You can't use an invalid Email or that is already in use by another veterinarian.
        /// </summary>
        /// <param name="newVeterinarianDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post(NewVeterinarianDTO newVeterinarianDTO)
        {
            if (newVeterinarianDTO == null) return BadRequest("Invalid data.");
            try
            {
                LoginModel userInfo = new LoginModel();
                userInfo.Email = newVeterinarianDTO.Email;
                userInfo.Password = newVeterinarianDTO.Password;
                var result = await _authentication.RegisterUser(userInfo.Email, userInfo.Password);
                
                if (result)
                {
                    var veterinarianExists = await _veterinarianService.GetByRegistrationAsync(newVeterinarianDTO.Registration);
                    if (veterinarianExists != null) return BadRequest("This veterinarian register is already registered.");
                    
                    await _veterinarianService.Add(newVeterinarianDTO);
                    
                    Response.StatusCode = 201;
                    return Ok($"Veterinary {userInfo.Email} was created successfully");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login attempt.");
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update an existing veterinarian entering with complete informations. Validations: You can't use an invalid Email or that is already in use by another veterinarian.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="veterinarianDTO"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, VeterinarianDTO veterinarianDTO)
        {
            if(id != veterinarianDTO.Id) return BadRequest("Invalid data.");
            if(veterinarianDTO == null) return BadRequest("Veterinarian object can not be null");
            try
            {                
                if(await _veterinarianService.VerifyIfExists(veterinarianDTO)) return BadRequest("There is another register with this register or email");
                await _veterinarianService.Update(veterinarianDTO);
                return Ok(veterinarianDTO);                
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deactive a veterinarian registration (Boolean Deletion).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var veterinarian = await _veterinarianService.GetByIdAsync(id);
            if(veterinarian == null) return NotFound("Veterinarian not found.");
            await _veterinarianService.Remove(id);
            return Ok(veterinarian);
        }
    }
}