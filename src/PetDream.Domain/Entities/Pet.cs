using System;
using PetDream.Domain.Validation;

namespace PetDream.Domain.Entities
{
    public sealed class Pet : BaseEntity
    {
        public string Name { get; private set; }
        public string Breed { get; private set; }
        public string Color { get; private set; }
        public string Gender { get; private set; }
        public DateTime BirthDate { get; private set; }
        public bool Status { get; private set; }
        public int PetOwnerId { get; set; }
        public PetOwner PetOwner { get; set; }

        public Pet(string name, string breed, string color, string gender, DateTime birthDate)
        {
            ValidateDomain(name, breed, color, gender, birthDate);
        }

        public Pet(int id, string name, string breed, string color, string gender, DateTime birthDate)
        {
            DomainExceptionValidation.When(id < 0,"Invalid id value.");
            Id = id;
            ValidateDomain(name, breed, color, gender, birthDate);         
        }

        public void Update(string name, string breed, string color, string gender, DateTime birthDate, bool status, int petOwnerId)
        {
            ValidateDomain(name, breed, color, gender, birthDate);
            PetOwnerId = petOwnerId;
            Status = status;
        }
        public void Delete()
        {
            Status = false;
        }

        private void ValidateDomain(string name, string breed, string color, string gender, DateTime birthDate)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name),"Invalid name. Name is required.");
            DomainExceptionValidation.When(name.Length < 2,"Invalid name. Name is too short, minimum 2 characters");        
            DomainExceptionValidation.When(string.IsNullOrEmpty(breed),"Invalid breed. Breed is required.");
            DomainExceptionValidation.When(breed.Length < 2,"Invalid breed. Breed is too short, minimum 2 characters");
            DomainExceptionValidation.When(string.IsNullOrEmpty(color),"Invalid color. Color is required.");
            DomainExceptionValidation.When(color.Length < 3,"Invalid color. Color is too short, minimum 3 characters");
            DomainExceptionValidation.When(string.IsNullOrEmpty(gender),"Invalid gender. Gender is required.");
            
            Name = name;
            Breed = breed;
            Color = color;
            Gender = gender;
            BirthDate = birthDate;
            Status = true;
        }

    }
}