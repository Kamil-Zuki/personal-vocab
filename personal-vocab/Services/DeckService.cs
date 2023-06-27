using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personal_vocab.DAL.DataAccess;
using personal_vocab.DAL.Entitis;
using personal_vocab.DTOs;
using personal_vocab.Interfeces;


namespace personal_vocab.Services
{
    public class DeckService : IDeckService
    {
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DeckService(DataContext dataContext, IHttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task CreateAsync(NoIdDeckDTO noIdDeck)
        {
            try
            {
                await _dataContext.Decks.AddAsync(new Deck()
                {
                    Name = noIdDeck.Name,
                    GroupId = noIdDeck.GroupId ?? _dataContext.Groups.FirstOrDefault(e => e.Name == "Default").Id,
                });
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<DeckDTO>> GetAsync()
        {
            try
            {
                return (await _dataContext.Decks.ToListAsync()).Select(x => new DeckDTO 
                {
                    Id = x.Id,
                    Name = x.Name, 
                    GroupId = x.GroupId 
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<DeckDTO> GetAsync(int id)
        {
            try
            {
                var deck = await _dataContext.Decks.FindAsync(id);

                return new DeckDTO()
                {
                    Id = deck.Id,
                    Name = deck.Name,
                    GroupId = deck.GroupId
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task PatchAsync(int id, JsonPatchDocument<DeckDTO> patchDoc)
        {
            try
            {
                var existingDeck = _dataContext.Decks.FirstOrDefault(e => e.Id == id);

                if (existingDeck == null)
                    throw new Exception("The deck hasn't been found.");

                var deckDTO = new DeckDTO()
                {
                    Id = existingDeck.Id,
                    Name = existingDeck.Name,
                    GroupId = existingDeck.GroupId
                };
                patchDoc.ApplyTo(deckDTO);

                existingDeck.Id = deckDTO.Id;
                existingDeck.Name = deckDTO.Name;
                existingDeck.GroupId = deckDTO.GroupId;

                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
        public async Task DeleteAsync(int id)
        {
            try
            {
                var deck = await _dataContext.Decks.FirstOrDefaultAsync(e => e.Id == id);
                if (deck != null)
                {
                    _dataContext.Decks.Remove(deck);
                    await _dataContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
