using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using sm_repetition_algorithm.DTOs;

namespace sm_repetition_algorithm.Interfeces
{
    public interface IGroupSevice
    {
        Task CreateAsync(NoIdGroupDTO group);
        Task<List<GroupDTO>> GetAsync();
        Task<GroupDTO> GetAsync(int id);
        Task PatchAsync(int id, [FromBody] JsonPatchDocument<GroupDTO> patchDoc);
        Task DeleteAsync(int id);
    }
}
