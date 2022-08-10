using System.Collections.Generic;
using System.Threading.Tasks;
using PetDream.Domain.Entities;

namespace PetDream.Domain.Interfaces
{
    public interface IVeterinaryCareRecordRepository
    {
        Task<IEnumerable<VeterinaryCareRecord>> GetAsync();
        Task<VeterinaryCareRecord> GetByIdAsync(int? id);
        Task<VeterinaryCareRecord> AddAsync(VeterinaryCareRecord veterinaryCareRecord);
        Task<VeterinaryCareRecord> UpdateAsync(VeterinaryCareRecord veterinaryCareRecord);
        
    }
}