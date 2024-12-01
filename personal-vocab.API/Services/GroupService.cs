using AutoMapper;
using personal_vocab.DAL.DataAccess;
using personal_vocab.DAL.Entitis;
using personal_vocab.DTOs.Requests;
using personal_vocab.DTOs.Responses;
using personal_vocab.Interfeces;

namespace personal_vocab.Services;

public class GroupService(DataContext dbContext, IMapper mapper) : IGroupSevice
{
    private readonly DataContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    public async Task<GroupDto> CreateAsync(CreateGroupDto model)
    {
        var group = _mapper.Map<Group>(model);
        _dbContext.Groups.Add(group);

        await dbContext.SaveChangesAsync();

        return _mapper.Map<GroupDto>(group);
    }
}



