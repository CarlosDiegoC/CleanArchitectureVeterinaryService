using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PetDream.Application.DTOs;
using PetDream.Application.Interfaces;
using PetDream.Domain.Entities;
using PetDream.Domain.Interfaces;

namespace PetDream.Application.Services
{
    public class VeterinarianService : IVeterinarianService
    {
        private IVeterinarianRepository _veterinarianRepository;
        private readonly IMapper _mapper;

        public VeterinarianService(IVeterinarianRepository veterinarianRepository, IMapper mapper)  
        {
            _veterinarianRepository = veterinarianRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VeterinarianDTO>> GetAsync()
        {
            var veterinarians = await _veterinarianRepository.GetAsync();
            return _mapper.Map<IEnumerable<VeterinarianDTO>>(veterinarians);
        }

        public async Task<VeterinarianDTO> GetByIdAsync(int? id)
        {
           var veterinarian = await _veterinarianRepository.GetByIdAsync(id);
           return _mapper.Map<VeterinarianDTO>(veterinarian);
        }

        public async Task<VeterinarianDTO> GetByEmailAsync(string email)
        {
           var veterinarian = await _veterinarianRepository.GetByEmailAsync(email);
           return _mapper.Map<VeterinarianDTO>(veterinarian);
        }

        public async Task<VeterinarianDTO> GetByRegistrationAsync(string registration)
        {
           var veterinarian = await _veterinarianRepository.GetByRegistrationAsync(registration);
           return _mapper.Map<VeterinarianDTO>(veterinarian);
        }

        public async Task Add(NewVeterinarianDTO newVeterinarianDTO)
        {
            var veterinarianEntity = _mapper.Map<Veterinarian>(newVeterinarianDTO);
            await _veterinarianRepository.AddAsync(veterinarianEntity);
        }

        public async Task Update(VeterinarianDTO veterinarianDto)
        {
            var veterinarianEntity = _mapper.Map<Veterinarian>(veterinarianDto);
            await _veterinarianRepository.UpdateAsync(veterinarianEntity);
        }

        public async Task Remove(int? id)
        {
            var veterinarian = _veterinarianRepository.GetByIdAsync(id).Result;
            await _veterinarianRepository.DeleteAsync(veterinarian);
        }

        public async Task <bool> VerifyIfExists(VeterinarianDTO veterinarianDTO)
        {
            var veterinarians = await _veterinarianRepository.GetAsync();
            var exists = veterinarians.ToList()
                .Exists(vet => vet.Id != veterinarianDTO.Id && (vet.Registration.Equals(veterinarianDTO.Registration) || vet.Email.Equals(veterinarianDTO.Email)));
            return exists;          
        }
    }
}