using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PetDream.Domain.Entities;
using PetDream.Domain.Interfaces;
using PetDream.Infra.Data.Context;

namespace PetDream.Infra.Data.Repositories
{
    public class VeterinaryCareRecordRepository : IVeterinaryCareRecordRepository
    {
        private readonly ApplicationDbContext _veterinaryCareRecordRepository;
        public VeterinaryCareRecordRepository(ApplicationDbContext context)
        {
            _veterinaryCareRecordRepository = context;
        }

        public async Task<VeterinaryCareRecord> AddAsync(VeterinaryCareRecord veterinaryCareRecord)
        {
            _veterinaryCareRecordRepository.Add(veterinaryCareRecord);
            await _veterinaryCareRecordRepository.SaveChangesAsync();
            return veterinaryCareRecord;

        }
        
        public async Task<IEnumerable<VeterinaryCareRecord>> GetAsync()
        {
            return await _veterinaryCareRecordRepository.VeterinaryCareRecords
                .Include(vet => vet.Pet.PetOwner)
                    .Include(vet => vet.Veterinarian)
                        .ToListAsync();
        }

        public async Task<VeterinaryCareRecord> GetByIdAsync(int? id)
        {
            return await _veterinaryCareRecordRepository.VeterinaryCareRecords
                .Include(vet => vet.Pet.PetOwner)
                    .Include(vet => vet.Veterinarian)
                        .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<VeterinaryCareRecord> UpdateAsync(VeterinaryCareRecord veterinaryCareRecord)
        {
            _veterinaryCareRecordRepository.Update(veterinaryCareRecord);
            await _veterinaryCareRecordRepository.SaveChangesAsync();
            return veterinaryCareRecord;
        }
    }
}