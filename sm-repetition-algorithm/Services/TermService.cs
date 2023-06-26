using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sm_repetition_algorithm.DAL.DataAccess;
using sm_repetition_algorithm.DAL.Entitis;
using sm_repetition_algorithm.DTOs;
using sm_repetition_algorithm.Interfeces;

namespace sm_repetition_algorithm.Services
{
    public class TermService : ITermService
    {
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TermService(DataContext dataContext, IHttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task CreateAsync(NoIdTermDTO noIdTerm)
        {
            try
            {
                var deckTerm = new DeckTerms()
                {
                    DeckId = noIdTerm.DeckId,
                    CreationDate = DateTime.UtcNow
                };
                await _dataContext.DeckTerms.AddAsync(deckTerm);

                await _dataContext.Terms.AddAsync(new Term()
                {
                    Text = noIdTerm.Text,
                    Transcription = noIdTerm.Transcription,
                    Meaning = noIdTerm.Meaning,
                    Example = noIdTerm.Example,
                    Image = noIdTerm.Image,

                    DeckTerm = deckTerm
                });
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<TermDTO>> GetAsync()
        {
            try
            {
                var termDTOs = await _dataContext.DeckTerms
                .SelectMany(dt => dt.Terms.Select(t => new TermDTO
                {
                    Id = t.Id,
                    Text = t.Text,
                    Transcription = t.Transcription,
                    Meaning = t.Meaning,
                    Example = t.Example,
                    Image = t.Image,
                    DeckId = dt.DeckId
                }))
                .ToListAsync();

                return termDTOs;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<TermDTO> GetAsync(int id)
        {
            try
            {
                return await _dataContext.Terms
                    .Where(t => t.Id == id)
                    .Select(t => new TermDTO
                    {
                        Id = t.Id,
                        Text = t.Text,
                        Transcription = t.Transcription,
                        Meaning = t.Meaning,
                        Example = t.Example,
                        Image = t.Image,
                        DeckId = t.DeckTerm.DeckId
                    })
                    .FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateAsync(int id, [FromBody] JsonPatchDocument<TermDTO> patchDoc)
        {
            try
            {
                var existingDeckTerm = _dataContext.DeckTerms
                    .Include(dt => dt.Terms)
                    .FirstOrDefault(dt => dt.Terms.Any(t => t.Id == id));

                if (existingDeckTerm == null)
                    throw new Exception("The term hasn't been found in any Deck.");

                var existingTerm = existingDeckTerm.Terms.FirstOrDefault(t => t.Id == id);

                if (existingTerm == null)
                    throw new Exception("The term hasn't been found.");

                var termDTO = new TermDTO()
                {
                    Id = existingTerm.Id,
                    Text = existingTerm.Text,
                    Transcription = existingTerm.Transcription,
                    Meaning = existingTerm.Meaning,
                    Example = existingTerm.Example,
                    Image = existingTerm.Image,
                    DeckId = existingDeckTerm.DeckId
                };

                patchDoc.ApplyTo(termDTO);

                existingTerm.Text = termDTO.Text;
                existingTerm.Transcription = termDTO.Transcription;
                existingTerm.Meaning = termDTO.Meaning;
                existingTerm.Example = termDTO.Example;
                existingTerm.Image = termDTO.Image;

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
                var existingTerm = await _dataContext.Terms.Include(t => t.DeckTerm)
                    .FirstOrDefaultAsync(t => t.Id == id);

                if (existingTerm == null)
                    throw new Exception("The term hasn't been found.");

                _dataContext.DeckTerms.RemoveRange(existingTerm.DeckTerm);
                _dataContext.Terms.Remove(existingTerm);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
