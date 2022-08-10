using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetDream.Application.DTOs;
using PetDream.Application.Interfaces;

namespace PetDream.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VeterinaryCareRecordsController : ControllerBase
    {
        private readonly IVeterinaryCareRecordService _veterinaryCareRecordService;

        public VeterinaryCareRecordsController(IVeterinaryCareRecordService veterinaryCareRecordService)
        {
            _veterinaryCareRecordService = veterinaryCareRecordService;
        }

        /// <summary>
        /// Get the full list of veterinary care records.
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Veterinary")]
        public async Task<IActionResult> Get()
        {
            var veterinarians = await _veterinaryCareRecordService.Get();
            return Ok(veterinarians);
        }

        /// <summary>
        /// Get a veterinary care record by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var veterinarian = await _veterinaryCareRecordService.GetById(id);
            return Ok(veterinarian);
        }

        /// <summary>
        /// Add a new veterynary care record.
        /// </summary>
        /// <param name="newVeterinaryCareRecordDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Veterinary")]
        public async Task<ActionResult> Post(NewVeterinaryCareRecordDTO newVeterinaryCareRecordDTO)
        {
            if(newVeterinaryCareRecordDTO == null) return BadRequest("Invalid data.");
            await _veterinaryCareRecordService.Add(newVeterinaryCareRecordDTO);
            return Ok(newVeterinaryCareRecordDTO);
        }

        
        /// <summary>
        /// Update an existing veterinary care record entering with complete informations.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="_veterinaryCareRecordDTO"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Veterinary")]
        public async Task<ActionResult> Put(int id, VeterinaryCareRecordDTO _veterinaryCareRecordDTO)
        {
            if(id != _veterinaryCareRecordDTO.Id) return BadRequest("Invalid data.");
            if(_veterinaryCareRecordDTO == null) return NotFound("Veterinarian not found.");
            await _veterinaryCareRecordService.Update(_veterinaryCareRecordDTO);
            return Ok(_veterinaryCareRecordDTO);
        }

    }
}