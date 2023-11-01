using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using personal_vocab.DTOs;
using personal_vocab.Interfeces;

namespace personal_vocab.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/deck")]
    public class DeckController : ControllerBase
    {
        private readonly IDeckService _deckService;
        public DeckController(IDeckService deckService) 
        {
            _deckService = deckService;
        }

        [HttpPost]
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
        [HttpGet]
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
        [HttpGet("{id}")]
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
        [HttpPatch("{id}")]
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
        [HttpDelete]
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
