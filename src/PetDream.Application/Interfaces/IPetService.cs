using System.Collections.Generic;
using System.Threading.Tasks;
using PetDream.Application.DTOs;

namespace PetDream.Application.Interfaces
{
    public interface IPetService
    {
        Task<IEnumerable<PetDTO>> Get();
        Task<PetDTO> GetById(int? id);
        Task Add(NewPetDTO newPetDto);
        Task Update(UpdatePetDTO updatePetDTO);
        Task Remove(int? id);
    }
}