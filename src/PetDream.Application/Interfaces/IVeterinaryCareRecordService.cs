using System.Collections.Generic;
using System.Threading.Tasks;
using PetDream.Application.DTOs;

namespace PetDream.Application.Interfaces
{
    public interface IVeterinaryCareRecordService
    {
       Task<IEnumerable<VeterinaryCareRecordDTO>> Get();
        Task<VeterinaryCareRecordDTO> GetById(int? id);
        Task Add(NewVeterinaryCareRecordDTO newVeterinaryCareRecordDTO);
        Task Update(VeterinaryCareRecordDTO veterinaryCareRecordDto);
    }
}