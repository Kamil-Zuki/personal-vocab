using Microsoft.AspNetCore.Mvc;
using sm_repetition_algorithm.BLL.DTOs;
using sm_repetition_algorithm.BLL.Interfeces;

namespace sm_repetition_algorithm.Controllers
{
    [ApiController]
    [Route("/v1/repetition")]
    public class RepetitionController : ControllerBase
    {
        ISuperMemo2Algorithm _superMemoAlgorithm;

        public RepetitionController(ISuperMemo2Algorithm superMemoAlgorithm)
        {
            _superMemoAlgorithm = superMemoAlgorithm;
        }

        [HttpGet]
        public IActionResult Repetition(FlashCard card, int quality)
        {
            try
            {
                _superMemoAlgorithm.CalculateSuperMemo2Algorithm(card, quality);
                return Ok();
            }
            catch(Exception ex)
            {
                return Problem(ex.StackTrace, ex.Message);
            }
        }
    }
}
