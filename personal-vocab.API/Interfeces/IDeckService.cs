using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using personal_vocab.DTOs;

namespace personal_vocab.Interfeces
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
