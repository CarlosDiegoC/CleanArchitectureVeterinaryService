using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetDream.Application.DTOs
{
    public class UpdatePetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public string Color { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public int PetOwnerId { get; set; }
    }
}