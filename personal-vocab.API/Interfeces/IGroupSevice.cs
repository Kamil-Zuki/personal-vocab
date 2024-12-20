﻿using personal_vocab.DTOs.Requests;
using personal_vocab.DTOs.Responses;

namespace personal_vocab.Interfeces;

public interface IGroupSevice
{
    Task<GroupDto> CreateAsync(Guid userId, CreateGroupDto model);
    Task<IEnumerable<GroupDto>> GetAllAsync(Guid userId);
    Task<GroupDto> GetByIdAsync(Guid id, Guid userId);
    Task<GroupDto> UpdateAsync(Guid id, CreateGroupDto model);
    Task<bool> DeleteAsync(Guid id);
}

