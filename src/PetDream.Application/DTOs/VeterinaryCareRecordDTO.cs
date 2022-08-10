using System;
using PetDream.Domain.Entities;

namespace PetDream.Application.DTOs
{
    public class VeterinaryCareRecordDTO
    {
        public int Id { get; set; }
        public DateTime ServiceDate { get; set; }
        public float PetWeight { get; set; }
        public string PetObservations { get; set; }
        public string Diagnosis { get; set; }
        public Pet Pet { get; set; }
        public Veterinarian Veterinarian { get; set; }
    }
}