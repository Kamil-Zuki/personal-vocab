using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using sm_repetition_algorithm.DTOs;

namespace sm_repetition_algorithm.Interfeces
{
    public interface IDeckService
    {
        Task CreateAsync(NoIdDeckDTO noIdDeck);
        Task<List<DeckDTO>> GetAsync();
        Task<DeckDTO> GetAsync(int id);
        Task PatchAsync(int id, [FromBody] JsonPatchDocument<DeckDTO> patchDoc);
        Task DeleteAsync(int id);

    }
}
