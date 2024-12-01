using personal_vocab.DTOs.Requests;
using personal_vocab.DTOs.Responses;

namespace personal_vocab.Interfeces;

public interface IGroupSevice
{
    Task<GroupDto> CreateAsync(CreateGroupDto model);
}

