using AutoMapper;
using Microsoft.EntityFrameworkCore;
using personal_vocab.DAL.DataAccess;
using personal_vocab.DAL.Entitis;
using personal_vocab.DTOs.Requests;
using personal_vocab.DTOs.Responses;
using personal_vocab.Interfeces;

namespace personal_vocab.Services;

public class TermService(DataContext dbContext, IMapper mapper) : ITermService
{
    private readonly DataContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    public async Task<TermDto> CreateAsync(CreateTermDto model)
    {
        var termDeck = new DeckTerms()
        {
            DeckId = model.DeckId,
            Term = _mapper.Map<Term>(model)
        };
        termDeck.Term.RepetitionData = new RepetitionData();

        _dbContext.DeckTerms.Add(termDeck);

        await dbContext.SaveChangesAsync();

        var termDto = _mapper.Map<TermDto>(termDeck.Term);
        termDto.DeckId = model.DeckId;

        return termDto;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var term = await _dbContext.Terms.FindAsync(id);
        if (term == null)
        {
            return false;
        }

        _dbContext.Terms.Remove(term);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<TermDto>> GetAllAsync()
    {
        var terms = await _dbContext.Terms.ToListAsync();

        return _mapper.Map<IEnumerable<TermDto>>(terms);
    }

    public async Task<TermDto> GetByIdAsync(Guid id)
    {
        var term = await _dbContext.Terms.FindAsync(id);

        return _mapper.Map<TermDto>(term);
    }

    public async Task<TermDto> UpdateAsync(Guid id, CreateTermDto model)
    {
        var existingTerm = await _dbContext.Terms.FindAsync(id)
            ?? throw new KeyNotFoundException("Term not found.");

        _mapper.Map(model, existingTerm);

        await _dbContext.SaveChangesAsync();

        return _mapper.Map<TermDto>(existingTerm);
    }
}



