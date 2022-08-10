using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PetDream.Api.Models;

namespace PetDream.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class TheDogApiController : ControllerBase
    {        

        /// <summary>
        /// Return a list of breed from TheDogApi.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            HttpClient client = new HttpClient {BaseAddress = new Uri("https://api.thedogapi.com/v1/")};
            var response = await client.GetAsync("breeds");
            var content = await response.Content.ReadAsStringAsync();
            var breeds = JsonConvert.DeserializeObject<List<DogModel>>(content);
            return Ok(breeds);
        }

        /// <summary>
        /// Return a breed by name from TheDogApi.
        /// </summary>
        /// <param name="breed"></param>
        /// <returns></returns>
        [HttpGet("breed")]
        public async Task<IActionResult> GetByBreedName(string breed)
        {
            HttpClient client = new HttpClient {BaseAddress = new Uri("https://api.thedogapi.com/v1/")};
            client.DefaultRequestHeaders.Add("x-api-key", "f8544bf6-b5a3-4232-8550-6df9a6feefab");
            var response = await client.GetAsync($"breeds/search?q={breed}");
            var content = await response.Content.ReadAsStringAsync();
            var breedInformations = JsonConvert.DeserializeObject<DogModel[]>(content);
            return Ok(breedInformations);
        }

    }
}