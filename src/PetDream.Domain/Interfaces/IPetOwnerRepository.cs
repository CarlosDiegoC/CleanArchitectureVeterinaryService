using System.Collections.Generic;
using System.Threading.Tasks;
using PetDream.Domain.Entities;

namespace PetDream.Domain.Interfaces
{
    public interface IPetOwnerRepository
    {
        Task<IEnumerable<PetOwner>> GetAsync();
        Task<PetOwner> GetByIdAsync(int? id);
        Task<PetOwner> GetByCpfAsync(string cpf);
        Task<PetOwner> GetByEmailAsync(string email);
        Task<PetOwner> AddAsync(PetOwner petOwner);
        Task<PetOwner> UpdateAsync(PetOwner petOwner);
        Task<PetOwner> DeleteAsync(PetOwner petOwner);
    }
}