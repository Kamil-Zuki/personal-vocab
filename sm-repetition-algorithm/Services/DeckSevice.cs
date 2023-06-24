using sm_repetition_algorithm.DAL.DataAccess;
using sm_repetition_algorithm.Interfeces;

namespace sm_repetition_algorithm.Services
{
    public class DeckSevice : IDeckSevice
    {
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DeckSevice(DataContext dataContext, IHttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
        }


    }
}
