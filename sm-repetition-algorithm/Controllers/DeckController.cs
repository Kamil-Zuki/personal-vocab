using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using sm_repetition_algorithm.DTOs;
using sm_repetition_algorithm.Interfeces;

namespace sm_repetition_algorithm.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/deck")]
    public class DeckController : ControllerBase
    {
        private readonly IDeckService _deckService;
        public DeckController(IDeckService deckService) 
        {
            _deckService = deckService;
        }

        [HttpPost("v1")]
        public async Task<ActionResult> CreateAsync(NoIdDeckDTO noIdDeck)
        {
            try
            {
                await _deckService.CreateAsync(noIdDeck);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("v1")]
        public async Task<ActionResult> GetAsync()
        {
            try
            {
                return Ok(await _deckService.GetAsync());
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("v1/{id}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            try
            {
                return Ok(await _deckService.GetAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPatch("v1/{id}")]
        public async Task<ActionResult> PatchAsync(int id, [FromBody] JsonPatchDocument<DeckDTO> patchDoc)
        {
            try
            {
                await _deckService.PatchAsync(id,patchDoc);
                
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("v1")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                await _deckService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
