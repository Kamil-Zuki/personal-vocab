using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using personal_vocab.DTOs;

namespace personal_vocab.Interfeces
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
