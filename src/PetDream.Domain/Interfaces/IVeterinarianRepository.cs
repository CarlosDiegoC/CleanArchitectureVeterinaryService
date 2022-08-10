using System.Collections.Generic;
using System.Threading.Tasks;
using PetDream.Domain.Entities;

namespace PetDream.Domain.Interfaces
{
    public interface IVeterinarianRepository
    {
        Task<IEnumerable<Veterinarian>> GetAsync();
        Task<Veterinarian> GetByIdAsync(int? id);
        Task<Veterinarian> GetByRegistrationAsync(string registration);
        Task<Veterinarian> GetByEmailAsync(string email);
        Task<Veterinarian> AddAsync(Veterinarian veterinarian);
        Task<Veterinarian> UpdateAsync(Veterinarian veterinarian);
        Task<Veterinarian> DeleteAsync(Veterinarian veterinarian);
    }
}