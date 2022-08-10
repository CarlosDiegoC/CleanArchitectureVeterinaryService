using System.Collections.Generic;
using System.Threading.Tasks;
using PetDream.Application.DTOs;

namespace PetDream.Application.Interfaces
{
    public interface IPetOwnerService
    {
        Task<IEnumerable<PetOwnerDTO>> Get();
        Task<PetOwnerDTO> GetById(int? id);
        Task<PetOwnerDTO> GetByCpf(string cpf);
        Task<PetOwnerDTO> GetByEmail(string email);
        Task Add(NewPetOwnerDTO PetOwnerDto);
        Task Update(PetOwnerDTO petOwnerDto);
        Task Remove(int? id);
        Task <bool> VerifyIfExists(PetOwnerDTO petOwnerDTO);
    }
}