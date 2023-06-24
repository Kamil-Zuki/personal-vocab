using sm_repetition_algorithm.DAL.DataAccess;
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
    }
}
