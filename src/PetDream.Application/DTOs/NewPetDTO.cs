using System;

namespace PetDream.Application.DTOs
{
    public class NewPetDTO
    {
        public string Name { get; set; }
        public string Breed { get; set; }
        public string Color { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public int PetOwnerId { get; set; }
    }
}