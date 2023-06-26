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
                    DeckId = noIdTerm.DeckId
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
                var allTerms = await _dataContext.Terms.ToListAsync();
                return new List<TermDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task GetAsync(int id)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateAsync(TermDTO termDTO)
        {
            try
            {
                _dataContext.Terms.Update(new Term()
                {
                    Id = termDTO.Id,
                    Text = termDTO.Text,
                    Transcription = termDTO.Transcription,
                    Meaning = termDTO.Meaning, 
                    Example = termDTO.Example,
                    Image = termDTO.Image,
                    
                });
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task DeleteAsync()
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
