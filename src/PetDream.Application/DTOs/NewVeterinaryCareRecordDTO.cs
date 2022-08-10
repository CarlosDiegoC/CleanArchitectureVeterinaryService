using System;
namespace PetDream.Application.DTOs
{
    public class NewVeterinaryCareRecordDTO
    {
        public float PetWeight { get; set; }
        public string PetObservations { get; set; }
        public string Diagnosis { get; set; }
        public int PetId { get; set; }
        public int VeterinarianId { get; set; }
    }
}