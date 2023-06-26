using sm_repetition_algorithm.DTOs;

namespace sm_repetition_algorithm.Interfeces
{
    public interface ITermService
    {
        Task CreateAsync(NoIdTermDTO noIdTerm);
        Task<List<TermDTO>> GetAsync();
    }
}
