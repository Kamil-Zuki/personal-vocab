using personal_vocab.DTOs.Requests;
using personal_vocab.DTOs.Responses;

namespace personal_vocab.Interfeces;

public interface ITermService
{
    Task<TermDto> CreateAsync(CreateTermDto model);
    Task<IEnumerable<TermDto>> GetAllAsync(Guid userId);
    Task<TermDto> GetByIdAsync(Guid id);
    Task<IEnumerable<TermDto>> GetByDeckIdAsync(Guid deckId);
    Task<TermDto> UpdateAsync(Guid id, CreateTermDto model);
    Task<bool> DeleteAsync(Guid id);
}
