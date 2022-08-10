using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PetDream.Domain.Entities;
using PetDream.Domain.Interfaces;
using PetDream.Infra.Data.Context;

namespace PetDream.Infra.Data.Repositories
{
    public class VeterinarianRepository : IVeterinarianRepository
    {
        private readonly ApplicationDbContext _veterinarianContext;
        public VeterinarianRepository(ApplicationDbContext context)
        {
            _veterinarianContext = context;
        }

        public async Task<Veterinarian> AddAsync(Veterinarian veterinarian)
        {
            _veterinarianContext.Add(veterinarian);
            await _veterinarianContext.SaveChangesAsync();
            return veterinarian;
        }

        public async Task<Veterinarian> DeleteAsync(Veterinarian veterinarian)
        {
            var veterinarianFounded = await _veterinarianContext.Veterinarians.FindAsync(veterinarian.Id);
            veterinarianFounded.Delete();
            await _veterinarianContext.SaveChangesAsync();
            return veterinarianFounded;
        }

        public async Task<IEnumerable<Veterinarian>> GetAsync()
        {
            return await _veterinarianContext.Veterinarians.Where(vet => vet.Status == true).AsNoTracking().ToListAsync();
        }

        public async Task<Veterinarian> GetByIdAsync(int? id)
        {
            return await _veterinarianContext.Veterinarians.FindAsync(id);
        }

        public async Task<Veterinarian> GetByEmailAsync(string email)
        {
            return await _veterinarianContext.Veterinarians.FirstOrDefaultAsync(vet => vet.Email == email);
        }

        public async Task<Veterinarian> GetByRegistrationAsync(string registration)
        {
            return await _veterinarianContext.Veterinarians.FirstOrDefaultAsync(vet => vet.Registration == registration);
        }

        public async Task<Veterinarian> UpdateAsync(Veterinarian veterinarian)
        {
            _veterinarianContext.Update(veterinarian);
            await _veterinarianContext.SaveChangesAsync();
            return veterinarian;
        }
    }
}