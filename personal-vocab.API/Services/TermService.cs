using AutoMapper;
using Microsoft.EntityFrameworkCore;
using personal_vocab.DAL.DataAccess;
using personal_vocab.DAL.Entitis;
using personal_vocab.DTOs.Requests;
using personal_vocab.DTOs.Responses;
using personal_vocab.Interfeces;

namespace personal_vocab.Services;

public class TermService : ITermService
{
    private readonly DataContext _dbContext;
    private readonly IMapper _mapper;

    public TermService(DataContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<TermDto> CreateAsync(CreateTermDto model)
    {
        var deck = await _dbContext.Decks.FindAsync(model.DeckId);
        if (deck == null)
        {
            throw new ArgumentException($"Deck with ID {model.DeckId} does not exist.");
        }

        var term = _mapper.Map<Term>(model);
        term.RepetitionData = new RepetitionData();

        var deckTerm = new DeckTerms
        {
            DeckId = model.DeckId,
            Term = term
        };

        _dbContext.DeckTerms.Add(deckTerm);
        await _dbContext.SaveChangesAsync();

        var termDto = _mapper.Map<TermDto>(term);
        termDto.DeckId = model.DeckId;

        return termDto;
    }

    public async Task<IEnumerable<TermDto>> GetAllAsync(Guid userId)
    {
        var terms = await _dbContext.Terms
            .Where(x => x.DeckTerms.Any(x => x.Deck.Group.UserId == userId)).ToListAsync();
        return _mapper.Map<IEnumerable<TermDto>>(terms);
    }

    public async Task<IEnumerable<TermDto>> GetByDeckIdAsync(Guid deckId)
    {
        var deck = await _dbContext.Decks.FindAsync(deckId);
        if (deck == null)
        {
            throw new ArgumentException($"Deck with ID {deckId} does not exist.");
        }

        var terms = await _dbContext.DeckTerms
            .Where(dt => dt.DeckId == deckId)
            .Include(dt => dt.Term)
            .Select(dt => dt.Term)
            .ToListAsync();

        return _mapper.Map<IEnumerable<TermDto>>(terms);
    }

    public async Task<TermDto> GetByIdAsync(Guid id)
    {
        var term = await _dbContext.Terms.FindAsync(id);
        return _mapper.Map<TermDto>(term);
    }

    public async Task<TermDto> UpdateAsync(Guid id, CreateTermDto model)
    {
        var existingTerm = await _dbContext.Terms
            .Include(t => t.DeckTerms)
            .FirstOrDefaultAsync(t => t.Id == id)
            ?? throw new KeyNotFoundException("Term not found.");

        var deck = await _dbContext.Decks.FindAsync(model.DeckId);
        if (deck == null)
        {
            throw new ArgumentException($"Deck with ID {model.DeckId} does not exist.");
        }

        _mapper.Map(model, existingTerm);

        var existingDeckTerm = existingTerm.DeckTerms.FirstOrDefault(dt => dt.DeckId == model.DeckId);
        if (existingDeckTerm == null)
        {
            var newDeckTerm = new DeckTerms
            {
                DeckId = model.DeckId,
                Term = existingTerm
            };
            _dbContext.DeckTerms.Add(newDeckTerm);
        }

        await _dbContext.SaveChangesAsync();

        return _mapper.Map<TermDto>(existingTerm);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var term = await _dbContext.Terms
            .Include(t => t.DeckTerms)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (term == null)
        {
            return false;
        }

        _dbContext.DeckTerms.RemoveRange(term.DeckTerms);
        _dbContext.Terms.Remove(term);

        await _dbContext.SaveChangesAsync();
        return true;
    }
}
