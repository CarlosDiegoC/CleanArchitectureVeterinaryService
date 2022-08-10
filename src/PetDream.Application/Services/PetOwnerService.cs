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
    public class PetOwnerService : IPetOwnerService
    {
        private IPetOwnerRepository _petOwnerRepository;
        private readonly IMapper _mapper;

        public PetOwnerService(IPetOwnerRepository petOwnerRepository, IMapper mapper)
        {
            _petOwnerRepository = petOwnerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PetOwnerDTO>> Get()
        {
            var petOwners = await _petOwnerRepository.GetAsync();
            return _mapper.Map<IEnumerable<PetOwnerDTO>>(petOwners);
        }

        public async Task<PetOwnerDTO> GetById(int? id)
        {
            var petOwner = await _petOwnerRepository.GetByIdAsync(id);
            return _mapper.Map<PetOwnerDTO>(petOwner);
        }

        public async Task<PetOwnerDTO> GetByCpf(string cpf)
        {
            var petOwner = await _petOwnerRepository.GetByCpfAsync(cpf);
            return _mapper.Map<PetOwnerDTO>(petOwner);
        }

        public async Task<PetOwnerDTO> GetByEmail(string email)
        {
            var petOwner = await _petOwnerRepository.GetByEmailAsync(email);
            return _mapper.Map<PetOwnerDTO>(petOwner);
        }

        public async Task Add(NewPetOwnerDTO newPetOwnerDto)
        {
            var petOwnerEntity = _mapper.Map<PetOwner>(newPetOwnerDto);
            await _petOwnerRepository.AddAsync(petOwnerEntity);            
        }

        public async Task Update(PetOwnerDTO petOwnerDto)
        {
            var petOwnerEntity = _mapper.Map<PetOwner>(petOwnerDto);
            await _petOwnerRepository.UpdateAsync(petOwnerEntity); 
        }

        public async Task Remove(int? id)
        {
            var petOwner = _petOwnerRepository.GetByIdAsync(id).Result;
            await _petOwnerRepository.DeleteAsync(petOwner);
        }

        public async Task <bool> VerifyIfExists(PetOwnerDTO petOwnerDTO)
        {
            var petOwners = await _petOwnerRepository.GetAsync();
            var exists = petOwners.ToList().Exists(owner => owner.Id != petOwnerDTO.Id && (owner.Cpf.Equals(petOwnerDTO.Cpf) || owner.Email.Equals(petOwnerDTO.Email)));
            return exists;          
        }
    }
}