using AutoMapper;
using personal_vocab.DAL.Entitis;
using personal_vocab.DTOs.Requests;
using personal_vocab.DTOs.Responses;

namespace personal_vocab.AutoMapProfile;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateGroupDto, Group>().ReverseMap();
        CreateMap<GroupDto, Group>().ReverseMap();

        CreateMap<CreateDeckDto, Deck>().ReverseMap();
        CreateMap<DeckDto, Deck>().ReverseMap();

        CreateMap<CreateTermDto, Term>().ReverseMap();
        CreateMap<TermDto, Term>().ReverseMap();
    }
}
