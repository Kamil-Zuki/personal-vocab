using Microsoft.AspNetCore.Mvc;
using sm_repetition_algorithm.BLL.Interfeces;
using sm_repetition_algorithm.BLL.Models;

namespace sm_repetition_algorithm.Controllers
{
    [ApiController]
    [Route("repetition")]
    public class RepetitionController : ControllerBase
    {
        private readonly ISuperMemo2Algorithm _superMemoAlgorithm;

        public RepetitionController(ISuperMemo2Algorithm superMemoAlgorithm)
        {
            _superMemoAlgorithm = superMemoAlgorithm;
        }

        [HttpGet]
        public IActionResult Repetition([FromQuery] FlashCard card, int quality)
        {
            try
            {
                _superMemoAlgorithm.CalculateSuperMemo2Algorithm(card, quality);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.StackTrace, ex.Message);
            }
        }
    }
}
