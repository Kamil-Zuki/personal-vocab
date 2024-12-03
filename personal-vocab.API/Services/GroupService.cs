using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public async Task<GroupDto> CreateAsync(Guid userId, CreateGroupDto model)
    {
        var group = _mapper.Map<Group>(model);
        group.UserId = userId;
        _dbContext.Groups.Add(group);

        await dbContext.SaveChangesAsync();

        return _mapper.Map<GroupDto>(group);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var group = await _dbContext.Groups.FindAsync(id);
        if (group == null)
        {
            return false;
        }

        _dbContext.Groups.Remove(group);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<GroupDto>> GetAllAsync(Guid userId)
    {
        var groups = await _dbContext.Groups
            .Where(x => x.UserId == userId)
            .ToListAsync();

        return _mapper.Map<IEnumerable<GroupDto>>(groups);
    }

    public async Task<GroupDto> GetByIdAsync(Guid id, Guid userId)
    {
        var group = await _dbContext.Groups.FirstOrDefaultAsync(x => x.UserId == userId && x.Id == id);

        return _mapper.Map<GroupDto>(group);
    }

    public async Task<GroupDto> UpdateAsync(Guid id, CreateGroupDto model)
    {
        var existingGroup = await _dbContext.Groups.FindAsync(id)
            ?? throw new KeyNotFoundException("Group not found.");

        _mapper.Map(model, existingGroup);

        await _dbContext.SaveChangesAsync();

        return _mapper.Map<GroupDto>(existingGroup);
    }
}



