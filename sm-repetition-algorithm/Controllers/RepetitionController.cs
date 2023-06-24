using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sm_repetition_algorithm.BLL.Interfeces;
using sm_repetition_algorithm.BLL.Models;

namespace sm_repetition_algorithm.Controllers
{
    [Authorize]
    [ApiController]
    [Route("repetition")]
    public class RepetitionController : ControllerBase
    {
        private readonly IFlashCardSevice _superMemoAlgorithm;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        public RepetitionController(IFlashCardSevice superMemoAlgorithm/*, IHttpContextAccessor httpContextAccessor*/)
        {
            _superMemoAlgorithm = superMemoAlgorithm;
            //_httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("v1/learn")]
        public async Task<ActionResult> Repetition([FromQuery] FlashCard card)
        {
            try
            {
                //var asd = _httpContextAccessor.HttpContext?.User;
                await _superMemoAlgorithm.Learn(card);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.StackTrace, ex.Message);
            }
        }



    }
}
