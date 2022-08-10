using System.Collections.Generic;
using System.Threading.Tasks;
using PetDream.Domain.Entities;

namespace PetDream.Domain.Interfaces
{
    public interface IPetRepository
    {
        Task<IEnumerable<Pet>> GetAsync();
        Task<Pet> GetByIdAsync(int? id);
        Task<Pet> AddAsync(Pet pet);
        Task<Pet> UpdateAsync(Pet pet);
        Task<Pet> DeleteAsync(Pet pet);
    }
}