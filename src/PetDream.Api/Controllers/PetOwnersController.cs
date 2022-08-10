using System;
using System.Collections.Generic;
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
    public class PetOwnersController : ControllerBase
    {
        private readonly IPetOwnerService _petOwnerService;
        private readonly IAuthenticate _authentication;

        public PetOwnersController(IAuthenticate authentication, IPetOwnerService petOwnerService)
        {
            _petOwnerService = petOwnerService;
            _authentication = authentication ?? throw new ArgumentException(nameof(authentication));
        }

        
        /// <summary>
        /// Add a new pet owner. Validations: You can't use an invalid CPF/Email or that is already in use by another pet owner.
        /// </summary>
        /// <param name="newPetOwnerDTO"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Post(NewPetOwnerDTO newPetOwnerDTO)
        {
            if(newPetOwnerDTO == null) return BadRequest("Invalid data.");
            try
            {
                LoginModel userInfo = new LoginModel();                    
                userInfo.Email = newPetOwnerDTO.Email;
                userInfo.Password = newPetOwnerDTO.Password;
                var result = await _authentication.RegisterUser(userInfo.Email, userInfo.Password);

                if (result)
                {
                    var petOwnerExistingCpf = await _petOwnerService.GetByCpf(newPetOwnerDTO.Cpf);
                    if (petOwnerExistingCpf != null) return BadRequest("This CPF is already registered.");
                    var petOwnerExistingEmail = await _petOwnerService.GetByEmail(newPetOwnerDTO.Email);
                    if (petOwnerExistingEmail != null) return BadRequest("This Email is already registered.");

                    await _petOwnerService.Add(newPetOwnerDTO);

                    Response.StatusCode = 201;
                    
                    return Ok($"User {userInfo.Email} was created successfully");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid register attempt.");
                    return BadRequest(ModelState);
                }                          
            }
            catch (System.Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get the full list of active pet owners.
        /// </summary>
        [HttpGet]
        public async Task <ActionResult<IEnumerable<PetOwnerDTO>>> Get()
        {
            var petOwners = await _petOwnerService.Get();
            var petOwnersList = petOwners.ToList();
            if(!petOwners.Any()) return BadRequest("There is no pet owners registered");
            return petOwnersList;
        }

        /// <summary>
        /// Get a pet owner by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [Authorize(Roles = "Veterinary")]
        public async Task<ActionResult<PetOwnerDTO>> GetById(int id)
        {
            var petOwner = await _petOwnerService.GetById(id);
            if(petOwner is null) return NotFound("There is no pet owner with this id");
            return petOwner;
        }

        /// <summary>
        /// Get a pet owner by cpf.
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        [HttpGet("cpf/{cpf}")]
        public async Task<ActionResult<PetOwnerDTO>> GetByCpf(string cpf)
        {
            var petOwner = await _petOwnerService.GetByCpf(cpf);
            if(petOwner is null) return NotFound("There is no pet owner with this cpf");
            return petOwner;
        }

        /// <summary>
        /// Get a pet owner by email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("email/{email}")]
        public async Task<ActionResult<PetOwnerDTO>> GetByEmail(string email)
        {
            var petOwner = await _petOwnerService.GetByEmail(email);
            if(petOwner is null) return NotFound("There is no pet owner with this email");
            return petOwner;
        }

        

        /// <summary>
        /// Update an existing pet owner entering with complete informations. Validations: You can't use an invalid CPF/Email or that is already in use by another pet owner.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="petOwnerDTO"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, PetOwnerDTO petOwnerDTO)
        {
            if(id != petOwnerDTO.Id) return BadRequest("Invalid data.");
            if(petOwnerDTO.Id != id) return BadRequest("Pet Owner not found.");
            try
            {
                var veterinarianExists = await _petOwnerService.VerifyIfExists(petOwnerDTO);
                if(veterinarianExists) return BadRequest("There is another pet owner with the same cpf or email");
                await _petOwnerService.Update(petOwnerDTO);
                return Ok(new {msg = "Pet Owner updated!", PetOwner = petOwnerDTO});
            }
            catch (System.Exception ex )
            {
                
                return BadRequest(ex.Message);
            }
            
        }

        /// <summary>
        /// Deactive a pet owner registration (Boolean Deletion).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var petOwner = await _petOwnerService.GetById(id);
            if(petOwner == null) return NotFound("Pet Owner not found.");
            await _petOwnerService.Remove(id);
            return Ok(new {msg = "Pet Owner deleted!", petOwner});
        }

    }
}