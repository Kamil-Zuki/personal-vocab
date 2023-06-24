using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace sm_repetition_algorithm.Controllers
{
    [Authorize]
    [ApiController]
    [Route("deck")]
    public class DeckController : ControllerBase
    {
        public DeckController() 
        {

        }

        [HttpPost("v1")]
        public async Task<ActionResult> Create()
        {
            try
            {
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("v1")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("v1/{id}")]
        public async Task<ActionResult> GetById()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("v1")]
        public async Task<ActionResult> Update()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("v1")]
        public async Task<ActionResult> Delete()
        {
            try
            {
                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
