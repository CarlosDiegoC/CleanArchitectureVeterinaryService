using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using PetDream.Application.DTOs;
using PetDream.Application.Interfaces;
using PetDream.Domain.Entities;
using PetDream.Domain.Interfaces;

namespace PetDream.Application.Services
{
    public class VeterinaryCareRecordService : IVeterinaryCareRecordService
    {
        private IVeterinaryCareRecordRepository _veterinaryCareRecordRepository;
        private readonly IMapper _mapper;

        public VeterinaryCareRecordService(IVeterinaryCareRecordRepository veterinaryCareRecordRepository, IMapper mapper)
        {
            _veterinaryCareRecordRepository = veterinaryCareRecordRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VeterinaryCareRecordDTO>> Get()
        {
            var veterinaryCareRecords = await _veterinaryCareRecordRepository.GetAsync();
            return _mapper.Map<IEnumerable<VeterinaryCareRecordDTO>>(veterinaryCareRecords);
        }

        public async Task<VeterinaryCareRecordDTO> GetById(int? id)
        {
            var veterinaryCareRecord = await _veterinaryCareRecordRepository.GetByIdAsync(id);
            return _mapper.Map<VeterinaryCareRecordDTO>(veterinaryCareRecord);
        }

        public async Task Add(NewVeterinaryCareRecordDTO newVeterinaryCareRecordDTO)
        {
            var veterinaryCareRecordEntity = _mapper.Map<VeterinaryCareRecord>(newVeterinaryCareRecordDTO);
            await _veterinaryCareRecordRepository.AddAsync(veterinaryCareRecordEntity);
        }

        public async Task Update(VeterinaryCareRecordDTO veterinaryCareRecordDto)
        {
            var veterinaryCareRecordEntity = _mapper.Map<VeterinaryCareRecord>(veterinaryCareRecordDto);
            await _veterinaryCareRecordRepository.UpdateAsync(veterinaryCareRecordEntity);
        }

    }
}