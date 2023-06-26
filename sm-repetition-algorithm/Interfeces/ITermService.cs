using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using sm_repetition_algorithm.DTOs;

namespace sm_repetition_algorithm.Interfeces
{
    public interface ITermService
    {
        Task CreateAsync(NoIdTermDTO noIdTerm);
        Task<List<TermDTO>> GetAsync();
        Task<TermDTO> GetAsync(int id);
        Task UpdateAsync(int id, JsonPatchDocument<TermDTO> patchDoc);
        Task DeleteAsync(int id);

    }
}
