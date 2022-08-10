using System;
using PetDream.Domain.Validation;

namespace PetDream.Domain.Entities
{
    public sealed class VeterinaryCareRecord : BaseEntity
    {
        public DateTime ServiceDate { get; private set; }
        public float PetWeight { get; private set; }
        public string PetObservations { get; private set; }
        public string Diagnosis { get; private set; }
        public int PetId { get; set; }
        public Pet Pet { get; set; }
        public int VeterinarianId { get; set; }
        public Veterinarian Veterinarian { get; set; }

        public VeterinaryCareRecord()
        {
            
        }
        public VeterinaryCareRecord(DateTime serviceDate, float petWeight, string petObservations, string diagnosis, int petId, int veterinarianId)
        {
            ValidadeDomain(petWeight, petObservations, diagnosis, petId, veterinarianId);
        }

        public VeterinaryCareRecord(int id, DateTime serviceDate, float petWeight, string petObservations, string diagnosis, int petId, int veterinarianId)
        {
            DomainExceptionValidation.When(id < 0,"Invalid id value.");
            Id = id;
            ValidadeDomain(petWeight, petObservations, diagnosis, petId, veterinarianId);
        }

        private void ValidadeDomain(float petWeight, string petObservations, string diagnosis, int petId, int veterinarianId)
        {
            DomainExceptionValidation.When(petWeight < 0 && petWeight > 160, "Pet weight invalid value.");
            DomainExceptionValidation.When(string.IsNullOrEmpty(diagnosis), "Diagnosis is required.");
            DomainExceptionValidation.When(petObservations.Length > 1024,"Invalid pet observations. Maximum 1024 characters.");
            DomainExceptionValidation.When(diagnosis.Length < 5 && diagnosis.Length > 1024,"Invalid diagnosis. Minimum 5 characters, maximum 1024.");
            DomainExceptionValidation.When(petId < 0, "Pet id invalid value.");
            DomainExceptionValidation.When(VeterinarianId < 0, "Veterinarian id invalid value.");

            ServiceDate = DateTime.Now;
            PetWeight = petWeight;
            PetObservations = petObservations ?? "No observations.";
            Diagnosis = diagnosis;
            PetId = PetId;
            VeterinarianId = veterinarianId;
        }
    }
}