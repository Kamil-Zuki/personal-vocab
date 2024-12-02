using personal_vocab.DTOs.Requests;
using personal_vocab.DTOs.Responses;

namespace personal_vocab.Interfeces;

public interface IDeckService
{
    Task<DeckDto> CreateAsync(CreateDeckDto model);
    Task<IEnumerable<DeckDto>> GetAllAsync();
    Task<DeckDto> GetByIdAsync(Guid id);
    Task<DeckDto> UpdateAsync(Guid id, CreateDeckDto model);
    Task<bool> DeleteAsync(Guid id);
}

