using sm_repetition_algorithm.DTOs;

namespace sm_repetition_algorithm.Interfeces
{
    public interface IGroupSevice
    {
        Task CreateAsync(GroupWithoutIdDTO group);
        Task<List<GroupDTO>> GetAllAsync();
        Task<GroupDTO> GetAsync(int id);
        Task UpdateAsync(GroupDTO group);
        Task DeleteAsync(int id);
    }
}
