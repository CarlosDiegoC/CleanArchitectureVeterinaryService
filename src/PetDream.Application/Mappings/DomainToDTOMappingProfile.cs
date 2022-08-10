using AutoMapper;
using PetDream.Application.DTOs;
using PetDream.Domain.Entities;

namespace PetDream.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Pet, PetDTO>().ReverseMap();
            CreateMap<Pet, NewPetDTO>().ReverseMap();
            CreateMap<Pet, UpdatePetDTO>().ReverseMap();
            CreateMap<PetOwner, PetOwnerDTO>().ReverseMap();
            CreateMap<PetOwner, NewPetOwnerDTO>().ReverseMap();
            CreateMap<Veterinarian, NewVeterinarianDTO>().ReverseMap();
            CreateMap<Veterinarian, VeterinarianDTO>().ReverseMap();
            CreateMap<VeterinaryCareRecord, VeterinaryCareRecordDTO>().ReverseMap();
            CreateMap<VeterinaryCareRecord, NewVeterinaryCareRecordDTO>().ReverseMap();
        }
    }
}