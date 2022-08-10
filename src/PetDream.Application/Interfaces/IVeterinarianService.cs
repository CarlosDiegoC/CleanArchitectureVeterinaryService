using System.Collections.Generic;
using System.Threading.Tasks;
using PetDream.Application.DTOs;

namespace PetDream.Application.Interfaces
{
    public interface IVeterinarianService
    {
        Task<IEnumerable<VeterinarianDTO>> GetAsync();
        Task<VeterinarianDTO> GetByIdAsync(int? id);
        Task<VeterinarianDTO> GetByRegistrationAsync(string registration);
        Task<VeterinarianDTO> GetByEmailAsync(string email);
        Task Add(NewVeterinarianDTO newVeterinarianDTO);
        Task Update(VeterinarianDTO veterinarianDto);
        Task Remove(int? id);
        Task <bool> VerifyIfExists(VeterinarianDTO veterinarianDTO);
    }
}