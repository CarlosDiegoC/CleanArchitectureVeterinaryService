using System;
using System.Text.Json.Serialization;
using PetDream.Domain.Entities;

namespace PetDream.Application.DTOs
{
    public class PetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public string Color { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Status { get; set; }
        public PetOwner PetOwner { get; set; }
    }
}