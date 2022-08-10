using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using PetDream.Application.DTOs;
using PetDream.Application.Interfaces;
using PetDream.Domain.Entities;
using PetDream.Domain.Interfaces;

namespace PetDream.Application.Services
{
    public class PetService : IPetService
    {
        private IPetRepository _petRepository;
        private readonly IMapper _mapper;
        public PetService(IPetRepository petRepository, IMapper mapper)
        {
            _petRepository = petRepository;
            _mapper = mapper;
        }
        public async Task Add(NewPetDTO newPetDto)
        {
            var petEntity = _mapper.Map<Pet>(newPetDto);
            await _petRepository.AddAsync(petEntity);
        }

        public async Task<IEnumerable<PetDTO>> Get()
        {
            var pets = await _petRepository.GetAsync();
            return _mapper.Map<IEnumerable<PetDTO>>(pets);
        }

        public async Task<PetDTO> GetById(int? id)
        {
            var pet = await _petRepository.GetByIdAsync(id);
            return _mapper.Map<PetDTO>(pet);
        }

        public async Task Remove(int? id)
        {
            var petEntity = _petRepository.GetByIdAsync(id).Result;            
            await _petRepository.DeleteAsync(petEntity);
        }

        public async Task Update(UpdatePetDTO updatePetDTO)
        {
            var petEntity = _mapper.Map<Pet>(updatePetDTO);
            await _petRepository.UpdateAsync(petEntity);
        }
    }
}