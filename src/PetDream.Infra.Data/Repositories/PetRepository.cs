using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PetDream.Domain.Entities;
using PetDream.Domain.Interfaces;
using PetDream.Infra.Data.Context;

namespace PetDream.Infra.Data.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly ApplicationDbContext _petContext;
        public PetRepository(ApplicationDbContext context)
        {
            _petContext = context;
        }

        public async Task<Pet> AddAsync(Pet pet)
        {
            _petContext.Add(pet);
            await _petContext.SaveChangesAsync();
            return pet;
        }

        public async Task<Pet> DeleteAsync(Pet pet)
        {
            
            var petFounded = await _petContext.Pets.FindAsync(pet.Id);
            petFounded.Delete();
            await _petContext.SaveChangesAsync();
            return petFounded;
        }

        public async Task<IEnumerable<Pet>> GetAsync()
        {
            return await _petContext.Pets.Where(p => p.Status == true).Include(pet => pet.PetOwner).ToListAsync();
        }

        public async Task<Pet> GetByIdAsync(int? id)
        {
            return await _petContext.Pets.Include(pet => pet.PetOwner).FirstOrDefaultAsync(pet => pet.Id == id);
        }

        public async Task<Pet> UpdateAsync(Pet pet)
        {
            _petContext.Update(pet);
            await _petContext.SaveChangesAsync();
            return pet;
        }
    }
}