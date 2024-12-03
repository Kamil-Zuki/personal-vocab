using AutoMapper;
using Microsoft.EntityFrameworkCore;
using personal_vocab.DAL.DataAccess;
using personal_vocab.DAL.Entitis;
using personal_vocab.DTOs.Requests;
using personal_vocab.DTOs.Responses;
using personal_vocab.Interfeces;

namespace personal_vocab.Services;

public class DeckService(DataContext dbContext, IMapper mapper) : IDeckService
{
    private readonly DataContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    public async Task<DeckDto> CreateAsync(CreateDeckDto model)
    {
        var deck = _mapper.Map<Deck>(model);
        _dbContext.Decks.Add(deck);

        await dbContext.SaveChangesAsync();

        return _mapper.Map<DeckDto>(deck);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var deck = await _dbContext.Decks.FindAsync(id);
        if (deck == null)
        {
            return false;
        }

        _dbContext.Decks.Remove(deck);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<DeckDto>> GetAllAsync(Guid userId)
    {
        var decks = await _dbContext.Decks
            .Where(x => x.Group.UserId == userId).ToListAsync();

        return _mapper.Map<IEnumerable<DeckDto>>(decks);
    }

    public async Task<DeckDto> GetByIdAsync(Guid id, Guid userId)
    {
        var deck = await _dbContext.Decks
            .FirstOrDefaultAsync(x => x.Group.UserId == userId && x.Id == id);

        return _mapper.Map<DeckDto>(deck);
    }

    public async Task<DeckDto> UpdateAsync(Guid id, CreateDeckDto model)
    {
        var existingDeck = await _dbContext.Decks.FindAsync(id)
            ?? throw new KeyNotFoundException("Deck not found.");

        _mapper.Map(model, existingDeck);

        await _dbContext.SaveChangesAsync();

        return _mapper.Map<DeckDto>(existingDeck);
    }
}



