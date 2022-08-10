using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PetDream.Domain.Entities;
using PetDream.Domain.Interfaces;
using PetDream.Infra.Data.Context;

namespace PetDream.Infra.Data.Repositories
{
    public class PetOwnerRepository : IPetOwnerRepository
    {
        private readonly ApplicationDbContext _petOwnerRepository;
        public PetOwnerRepository(ApplicationDbContext context)
        {
            _petOwnerRepository = context;
        }

        public async Task<PetOwner> AddAsync(PetOwner petOwner)
        {
            _petOwnerRepository.Add(petOwner);
            await _petOwnerRepository.SaveChangesAsync();
            return petOwner;
        }

        public async Task<PetOwner> DeleteAsync(PetOwner petOwner)
        {
            var petOwnerFounded = await _petOwnerRepository.PetOwners.FindAsync(petOwner.Id);
            petOwnerFounded.Delete();
            await _petOwnerRepository.SaveChangesAsync();
            return petOwnerFounded;
        }

        public async Task<IEnumerable<PetOwner>> GetAsync()
        {
            return await _petOwnerRepository.PetOwners.Where(p => p.Status == true)
                .AsNoTracking()
                    .ToListAsync();
        }

        public async Task<PetOwner> GetByIdAsync(int? id)
        {
            return await _petOwnerRepository.PetOwners.FindAsync(id);
        }

        public async Task<PetOwner> GetByCpfAsync(string cpf)
        {
            return await _petOwnerRepository.PetOwners.FirstOrDefaultAsync(petOwner => petOwner.Cpf == cpf);
        }

        public async Task<PetOwner> GetByEmailAsync(string email)
        {
            return await _petOwnerRepository.PetOwners.FirstOrDefaultAsync(petOwner => petOwner.Email == email);
        }

        public async Task<PetOwner> UpdateAsync(PetOwner petOwner)
        {
            _petOwnerRepository.Update(petOwner);
            await _petOwnerRepository.SaveChangesAsync();
            return petOwner;
        }
    }
}