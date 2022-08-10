using PetDream.Domain.Validation;

namespace PetDream.Domain.Entities
{
    public sealed class Veterinarian : BaseEntity
    {
        public string Name { get; private set; }
        public string Registration { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Phone { get; private set; }
        public bool Status { get; private set; }

        public Veterinarian(string name, string registration, string email, string password, string phone)  
        {
            ValidateDomain(name, registration, email, password, phone);           
        }

        public Veterinarian(int id, string name, string registration, string email, string password, string phone)  
        {
            DomainExceptionValidation.When(id < 0,"Invalid id value.");
            Id = id;
            ValidateDomain(name, registration, email, password, phone);
        }

        public void Update(string name, string registration, string email, string password, string phone)
        {
            ValidateDomain(name, registration, email, password, phone);
        }

        public void Delete()
        {
            Status = false;
        }

        private void ValidateDomain(string name, string registration, string email, string password, string phone)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name),"Invalid name. Name is required.");
            DomainExceptionValidation.When(name.Length < 3,"Invalid name. Name is too short, minimum 3 characters");        
            DomainExceptionValidation.When(string.IsNullOrEmpty(registration),"Invalid registration. Registration is required.");        
            DomainExceptionValidation.When(string.IsNullOrEmpty(email),"Invalid email. Email is required.");            
            DomainExceptionValidation.When(!ValidateEmail.IsValid(email),"Invalid email. Enter a valid email.");          
            DomainExceptionValidation.When(string.IsNullOrEmpty(password),"Invalid password. Password is required.");
            DomainExceptionValidation.When(password.Length < 5,"Invalid password. Password is too short, minimum 5 characters");
            DomainExceptionValidation.When(string.IsNullOrEmpty(phone),"Invalid phone. Phone is required.");           
            
            Name = name;
            Registration = registration;
            Email = email;
            Password = password;
            Phone = phone;
            Status = true;
        }

    }

}