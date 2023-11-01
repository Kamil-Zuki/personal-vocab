using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using personal_vocab.DTOs;
using personal_vocab.Interfeces;

namespace personal_vocab.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/term")]
    public class TermController : ControllerBase
    {
        private readonly ITermService _termService;
        public TermController(ITermService termService)
        {
            _termService = termService;
        }

        [HttpPost]
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
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            try
            {
                var termDTOs = await _termService.GetAsync();
                return Ok(termDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            try
            {
                var termDTO = await _termService.GetAsync(id);
                return Ok(termDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPatch]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody] JsonPatchDocument<TermDTO> patchDoc)
        {
            try
            {
                await _termService.UpdateAsync(id, patchDoc);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                await _termService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
