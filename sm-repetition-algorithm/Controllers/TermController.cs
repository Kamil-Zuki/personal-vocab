using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sm_repetition_algorithm.DTOs;
using sm_repetition_algorithm.Interfeces;
using sm_repetition_algorithm.Services;

namespace sm_repetition_algorithm.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/term")]
    public class TermController : ControllerBase
    {
        private readonly ITermService _termService;
        public TermController(ITermService termService)
        {
            _termService = termService;
        }

        [HttpPost("v1/")]
        public async Task<ActionResult> CreateAsync(NoIdTermDTO noIdTrerm)
        {
            try
            {
                await _termService.CreateAsync(noIdTrerm);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("v1")]
        public async Task<ActionResult> GetAsync()
        {
            try
            {
                await _termService.GetAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("v1/{id}")]
        public async Task<ActionResult> GetAsync(int id)
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
        public async Task<ActionResult> UpdateAsync()
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
        public async Task<ActionResult> DeleteAsync()
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
